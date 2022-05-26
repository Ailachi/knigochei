using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
    [Authorize(Roles="Admin,Customer")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {


            return new JsonResult("success");
        }
    }
}
