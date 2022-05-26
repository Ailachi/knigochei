using Knigochei.Models;
using Knigochei.Services.CartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Knigochei.Controllers
{
    [Authorize(Roles="Admin,Customer")]
    public class CartController : Controller
    {

        private ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            int userId = GetUserId();
            _cartService.CreateUserCartIfNotExists(userId);



            return View();
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


        public int GetUserId()
        {
            string userIdClaimValue = HttpContext.User.Claims
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .Select(claim => claim.Value)
                .FirstOrDefault();


            return Convert.ToInt32(userIdClaimValue);
        }
    }
}
