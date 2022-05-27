using Microsoft.AspNetCore.Mvc;

namespace Knigochei.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
