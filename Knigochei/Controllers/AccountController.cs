using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("LoginAndRegistration");
        }

        [HttpPost]
        public IActionResult Login(string username, string password) // create and use Login form
        {
            return View();

        }

        public IActionResult SignUp()
        {
            return View("LoginAndRegistration");

        }

        [HttpPost]
        public IActionResult SignUp(string username, string password) // create and use SignUp form
        {
            return View();

        }
    }

}
