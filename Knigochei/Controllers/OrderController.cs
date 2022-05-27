using Knigochei.Models;
using Knigochei.Services.BookService;
using Knigochei.Services.CartService;
using Knigochei.Services.OrderService;
using Knigochei.Services.UserService;
using Knigochei.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Knigochei.Controllers
{
	[Authorize(Roles = "Admin,Customer")]
	public class OrderController : Controller
	{
		private ICartService _cartService;
		private IBookService _bookService;
		private IOrderService _orderService;
		private IUserService _userService;

		public OrderController(ICartService cartService, IOrderService orderService, IUserService userService, IBookService bookService)
		{
			_cartService = cartService;
			_orderService = orderService;
			_userService = userService;
			_bookService = bookService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CreateOrder()
		{
			int userId = GetUserId();
			List<CartItem> cartItems = _cartService.GetAllCartItemsByCart(userId);
			User user = _userService.GetUserById(userId);
			IEnumerable<OrderItemViewModel> orderItemViewModels = ConvertCartItemsToOrderItemsViewModel(cartItems); 

			OrderViewModel orderViewModel = new OrderViewModel();
			orderViewModel.User = user;
			orderViewModel.OrderItems = orderItemViewModels;
			orderViewModel.TotalPrice = orderItemViewModels.Sum(item => item.TotalAmountPrice);


			return View(orderViewModel);
		}

		[HttpPost]
		public IActionResult CreateOrder(string pickUpAddress, int totalPrice)
		{
			int userId = GetUserId();
			List<CartItem> cartItems = _cartService.GetAllCartItemsByCart(userId);

			Order order = new Order();
			order.OrderDate = DateTime.Now;
			order.DeliveryDate = DateTime.Now.AddDays(7);
			order.UserId = userId;
			order.PickUpAddress = pickUpAddress;
			order.OrderStatusId = (int)OrderStatus.Created;
			order.TotalPrice = totalPrice;

			_orderService.CreateNewOrder(order);

			_orderService.CreateOrderItemsFromCartItems(order, cartItems);

			_cartService.ClearUserCart(userId);

			return new JsonResult("Success");
		}

		
		public IActionResult OrderHistory()
		{
			int userId = GetUserId();
			List<Order> orders = _orderService.GetOrdersByUser(userId);
			User user = _userService.GetUserById(userId);

			OrderHistoryViewModel orderHistory = new OrderHistoryViewModel();
			orderHistory.Orders = orders;
			orderHistory.User = user;

			return View(orderHistory);
		}
		public int GetUserId()
		{
			string userIdClaimValue = HttpContext.User.Claims
				.Where(claim => claim.Type == ClaimTypes.NameIdentifier)
				.Select(claim => claim.Value)
				.FirstOrDefault();


			return Convert.ToInt32(userIdClaimValue);
		}

		public IEnumerable<OrderItemViewModel> ConvertCartItemsToOrderItemsViewModel(IEnumerable<CartItem> cartItems)
		{
			return cartItems.Select(cartItem =>
			{
				OrderItemViewModel orderItemViewModel = new OrderItemViewModel();
				orderItemViewModel.Book = _bookService.GetBookById(cartItem.BookId);
				orderItemViewModel.CartItem = cartItem;
				orderItemViewModel.TotalAmountPrice = orderItemViewModel.Book.Price * cartItem.Amount;


				return orderItemViewModel;
			});
		}
	}
}
