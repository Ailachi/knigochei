using Knigochei.Models;
using Knigochei.Services.AuthorService;
using Knigochei.Services.BookService;
using Knigochei.Services.CartService;
using Knigochei.Services.GenreService;
using Knigochei.Services.OrderService;
using Knigochei.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Knigochei.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private IBookService _bookService;
		private ICartService _cartService;
		private IAuthorService _authorService;
		private IGenreService _genreService;
		private IOrderService _orderService;


		public AdminController(
			IBookService bookService, IAuthorService authorService, 
			IGenreService genreService, ICartService cartService,
			IOrderService orderService
		)
        {
			_bookService = bookService;
			_authorService = authorService;
			_genreService = genreService;
			_cartService = cartService;
			_orderService = orderService;
        }
		public IActionResult Index()
		{
			List<Gender> allGenders = _authorService.GetGenders();
			List<Genre> allGenres = _genreService.GetAllGenres();
			List<Book> books = _bookService.GetAllBooks();
			List<Author> authors = _authorService.GetAllAuthors();

			ViewData["genders"] = allGenders;
			ViewData["genres"] = allGenres;
			ViewData["books"] = books;
			ViewData["authors"] = authors;


			return View();
		}

		public IActionResult OrderReport()
        {
			string fileName = "ordersReport.xlsx";
			List<Order> orders = _orderService.GetAllOrders();

			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage())
			{
				var workSheet = package.Workbook.Worksheets.Add("Orders Report");

				workSheet.Cells[1, 1].Value = "Id";
				workSheet.Cells[1, 2].Value = "Order Date";
				workSheet.Cells[1, 3].Value = "Delivery Date";
				workSheet.Cells[1, 4].Value = "Pickup Address";
				workSheet.Cells[1, 5].Value = "Total Price";
				workSheet.Cells[1, 6].Value = "Order Status Id";
				workSheet.Cells[1, 7].Value = "User Id";

				for (int row = 2; row <= orders.Count + 1; row++)
				{
					workSheet.Cells[row, 1].Value = orders[row - 2].Id;
					workSheet.Cells[row, 2].Value = orders[row - 2].OrderDate.ToString("yyyy/MM/dd");
					workSheet.Cells[row, 3].Value = orders[row - 2].DeliveryDate;
					workSheet.Cells[row, 4].Value = orders[row - 2].PickUpAddress;
					workSheet.Cells[row, 5].Value = orders[row - 2].TotalPrice;
					workSheet.Cells[row, 6].Value = orders[row - 2].OrderStatusId;
					workSheet.Cells[row, 7].Value = orders[row - 2].UserId;

				}

				var stream = new MemoryStream(package.GetAsByteArray());

				return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

			}
        }

		[HttpPost]
		public IActionResult AddAuthor(Author author, int birthYear)
        {
			author.BirthDate = new DateTime(birthYear, 01, 01);
			author.AvatarImagePath = "~/images/noAvatar.jpg";
			_authorService.AddAuthor(author);

			return RedirectToAction("Index");
        }

		[HttpPost]
		public IActionResult AddBook(Book book)
		{
			book.BookDescription = "some description";
			book.CoverImagePath = "~/images/noCover.jpg";
			book.BookRank = 2f;
			book.Price = 4000;
			_bookService.AddBook(book);

			return RedirectToAction("Index");
		}

		[HttpDelete]
		public IActionResult DeleteAuthor(int authorId)
		{
			_authorService.DeleteAuthorById(authorId);
			return new JsonResult("success");
		}

		[HttpDelete]
		public IActionResult DeleteBook(int bookId)
		{
			_bookService.DeleteBookById(bookId);
			_cartService.DeleteAllCartItemsByBook(bookId);

			return new JsonResult("success");
		}
	}
}
