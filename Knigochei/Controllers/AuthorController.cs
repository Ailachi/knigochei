using Knigochei.Models;
using Knigochei.Services.AuthorService;
using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
    public class AuthorController : Controller
    {

        private IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public IActionResult Index()
        {
            List<Author> authors = _authorService.GetAllAuthors();

            return View(authors);
        }

    }
}
