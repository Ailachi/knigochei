using Knigochei.Models;
using Knigochei.Repository.BookRepo;
using Knigochei.Services.BookService;
using Knigochei.Services.GenreService;
using Knigochei.UnitOfWorkDapper;
using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
    public class CatalogController : Controller
    {
        private IBookService _bookService;
        private IGenreService _genreService;

        public CatalogController(IBookService bookService, IGenreService genreService)
        {
            _bookService = bookService;
            _genreService = genreService;
        }
        public IActionResult Index()
        {
            List<Book> allBooks = _bookService.GetAllBooks();
            List<Genre> allGenres = _genreService.GetAllGenres();

            // sorting
            // filtering
            ViewData["books"] = allBooks;
            ViewData["genres"] = allGenres;

            return View();
        }
    }
}
