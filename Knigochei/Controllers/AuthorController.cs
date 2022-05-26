using Knigochei.Forms;
using Knigochei.Models;
using Knigochei.Services.AuthorService;
using Knigochei.Services.GenreService;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Knigochei.Controllers
{
    public class AuthorController : Controller
    {

        private IAuthorService _authorService;
        private IGenreService _genreService;
        public AuthorController(IAuthorService authorService, IGenreService genreService)
        {
            _authorService = authorService;
            _genreService = genreService;
        }

        public IActionResult Index(AuthorFilterFormModel formModel)
        {
            List<Author> authors = _authorService.GetFilteredAuthorsByGenre(formModel.GenreId);
            _authorService.FilterAuthorsByFirstName(ref authors, formModel.FirstName);
            _authorService.FilterAuthorsByLastName(ref authors, formModel.LastName);
            _authorService.SortAuthorsByFirstNameDesc(ref authors, formModel.OrderByFirstNameDesc);

            List<Genre> allGenres = _genreService.GetAllGenres();

            ViewData["genres"] = allGenres;
            ViewData["authors"] = authors;

            return View();
        }

        



    }
}
