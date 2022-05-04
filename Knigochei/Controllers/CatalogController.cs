using Knigochei.Forms;
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
        public IActionResult Index(CatalogFilterFormModel model)
        {
            List<Book> books = _bookService.GetAllBooks();
            List<Genre> allGenres = _genreService.GetAllGenres();

            FilterBooksByPrice(ref books, model.PriceMin, model.PriceMax);
            FilterBooksByGenre(ref books, model.GenreId);

            SortBooksByDesc(ref books, model.OrderByPriceDesc);

            ViewData["books"] = books;
            ViewData["genres"] = allGenres;


            return View();
        }

        private void FilterBooksByGenre(ref List<Book> books, int genreId)
        {
            if(genreId != 0)
                books = books.Where(book => book.GenreId == genreId).ToList();
        }

        private void FilterBooksByPrice(ref List<Book> books, int minPrice, int maxPrice)
        {
            books = books.Where(book => book.Price >= minPrice && book.Price <= maxPrice).ToList();
        }

        private void SortBooksByDesc(ref List<Book> books, bool isSortByDesk)
        {
            books = isSortByDesk ? books.OrderByDescending(book => book.Price).ToList() : books.OrderBy(book => book.Price).ToList();
        }

        
    }
}
