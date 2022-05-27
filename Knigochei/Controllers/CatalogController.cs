using Knigochei.Forms;
using Knigochei.Models;
using Knigochei.Repository.BookRepo;
using Knigochei.Services.AuthorService;
using Knigochei.Services.BookService;
using Knigochei.Services.GenreService;
using Knigochei.UnitOfWorkDapper;
using Knigochei.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
    public class CatalogController : Controller
    {
        private IBookService _bookService;
        private IAuthorService _authorService;
        private IGenreService _genreService;

        public CatalogController(IBookService bookService, IGenreService genreService, IAuthorService authorService)
        {
            _bookService = bookService;
            _genreService = genreService;
            _authorService = authorService;
        }
        public IActionResult Index(CatalogFilterFormModel model)
        {
            List<Book> books = _bookService.GetAllBooks();
            List<Genre> allGenres = _genreService.GetAllGenres();

            _bookService.FilterBooksByPrice(ref books, model.PriceMin, model.PriceMax);
            _bookService.FilterBooksByGenre(ref books, model.GenreId);

            _bookService.SortBooksByDesc(ref books, model.OrderByPriceDesc);

            ViewData["books"] = books;
            ViewData["genres"] = allGenres;


            return View();
        }

        [HttpGet]
        [Route("Catalog/Book/{bookId:int}")]
        public IActionResult BookPage(int bookId)
        {
            Book book = _bookService.GetBookById(bookId);
            if (book.Id == 0) return View("Error");

            Author author = _authorService.FindAuthorByBook(book.Id);

            var model = new BookPageViewModel();
            model.Book = book;
            model.Author = author;

            return View(model);
        }


        
    }
}
