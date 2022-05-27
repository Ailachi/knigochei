using Knigochei.Models;
using Knigochei.Services.AuthorService;
using Knigochei.Services.BookService;
using Knigochei.Services.CartService;
using Knigochei.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Knigochei.Controllers
{
    [Authorize(Roles="Admin,Customer")]
    public class CartController : Controller
    {

        private ICartService _cartService;
        private IBookService _bookService;
        private IAuthorService _authorService;

        public CartController(ICartService cartService, IBookService bookService, IAuthorService authorService)
        {
            _cartService = cartService;
            _bookService = bookService;
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            int userId = GetUserId();
            _cartService.CreateUserCartIfNotExists(userId);

            List<CartItem> cartItems = _cartService.GetAllCartItemsByCart(userId);
            List<CartItemViewModel> cartItemViewModels = ConvertCartItemViewsToViewModel(cartItems).ToList();

            return View(cartItemViewModels);
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {
            int userId = GetUserId();
            _cartService.CreateUserCartIfNotExists(userId);

            if(_cartService.ExistsCartItem(userId, bookId))
            {
                _cartService.UpdateUserCartItemAmountOrIncrement(userId, bookId);
            } else
            {
                _cartService.AddBookToCart(userId, bookId);
            }

            return new JsonResult("success");
        }
        [HttpPost]
        public IActionResult UpdateCartItemAmount(CartItem cartItem)
        {
            int userId = GetUserId();
            _cartService.UpdateUserCartItemAmountOrIncrement(userId, cartItem.BookId, cartItem.Amount);

            return new JsonResult("Success");
        }

        [HttpDelete]
        public IActionResult DeleteFromCart(int bookId)
        {
            int userId = GetUserId();
            _cartService.DeleteFromUserCartByBook(userId, bookId);

            return new JsonResult("Deleted Successfully");
        }


        public int GetUserId()
        {
            string userIdClaimValue = HttpContext.User.Claims
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .Select(claim => claim.Value)
                .FirstOrDefault();


            return Convert.ToInt32(userIdClaimValue);
        }

        public IEnumerable<CartItemViewModel> ConvertCartItemViewsToViewModel(IEnumerable<CartItem> cartItems)
        {
            return cartItems.Select(cartItem =>
            {
                CartItemViewModel cartItemView = new CartItemViewModel();
                cartItemView.Book = _bookService.GetBookById(cartItem.BookId);
                cartItemView.Author = _authorService.GetAuthorById(cartItemView.Book.AuthorId);
                cartItemView.CartItem = cartItem;

                return cartItemView;
            });
        }
    }
}
