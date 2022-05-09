using Knigochei.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Knigochei.UnitOfWorkDapper;
using Knigochei.Repository.BookRepo;
using Microsoft.AspNetCore.Authorization;

namespace Knigochei.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUnitOfWork _uow;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        [Authorize(Roles="Customer,Admin")]
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult DeleteBook(int bookId)
        {
            IBookRepository bookRepository = _uow.BookRepository;
            bookRepository.Delete(bookId);

            List<Book> books = bookRepository.All().ToList();

            _uow.Commit();

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}