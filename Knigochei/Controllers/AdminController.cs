using Knigochei.Models;
using Knigochei.Services.AuthorService;
using Knigochei.Services.BookService;
using Knigochei.Services.CartService;
using Knigochei.Services.GenreService;
using Knigochei.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private IBookService _bookService;
		private ICartService _cartService;
		private IAuthorService _authorService;
		private IGenreService _genreService;


		public AdminController(IBookService bookService, IAuthorService authorService, IGenreService genreService, ICartService cartService)
        {
			_bookService = bookService;
			_authorService = authorService;
			_genreService = genreService;
			_cartService = cartService;
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
