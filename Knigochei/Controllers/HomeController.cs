using Knigochei.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Knigochei.UnitOfWorkDapper;
using Knigochei.Repository.BookRepo;


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
            IBookRepository bookRepository = _uow.BookRepository;
            bookRepository.Add(new Book("Title2", "BookDescription", 1921, 2000, 1, 1, 8.4f));

            ViewBag.books = bookRepository.All();
            

            return View();
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