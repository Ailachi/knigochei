using Knigochei.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Knigochei.Services.UserService;
using Knigochei.Models;
using Microsoft.AspNetCore.Authorization;

namespace Knigochei.Controllers
{
    public class AccountController : Controller
    {

        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Login()
        {
            ViewData["Title"] = "Login";
            return View("LoginAndRegistration");
        }

        public IActionResult SignUp()
        {
            return View("LoginAndRegistration");

        }

        [HttpPost]
        public IActionResult SignUp(RegistrationFormModel model) // create and use SignUp form
            {
            if(!ModelState.IsValid)
            {
                ViewData["RegistrationError"] = "Wrong data format!";
                return View("LoginAndRegistration");
            }

            if(_userService.ExistsByEmail(model.Email))
            {
                ViewData["RegistrationError"] = "User with such email already exists!";
                return View("LoginAndRegistration");
            }

            _userService.CreateNewUser(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel model) // create and use Login form
        {
            if (!ModelState.IsValid)
            {
                ViewData["LoginError"] = "Email and password cannot be empty fields!";
                Console.WriteLine("Empty email or password");
                return View("LoginAndRegistration");
            }

            User user = _userService.GetUserByEmailAndPassword(model.Email, model.Password);
            
            if (user is null || user.Id == 0)
            {
                ViewData["LoginError"] = "Incorrect email or password!";
                Console.WriteLine("Incorrect email or password!");
                return View("LoginAndRegistration");
            }

            string userRole = _userService.GetUserRole(user.RoleId);
            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email,model.Email),
                new Claim(ClaimTypes.Role,userRole),
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}")
            }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if(userRole == "Admin") return RedirectToAction("Index", "Admin");

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Customer,Admin")]
        [Route("Account/")]
        [Route("Account/Index")]
        public IActionResult Profile()
        {
            int userId = GetUserId();
            User user = _userService.GetUserById(userId);


            return View(user);
        }

        [Authorize(Roles = "Customer,Admin")]
        public IActionResult EditProfile(User user)
		{
            user.Id = GetUserId();
            _userService.EditUser(user);

            return RedirectToAction("Profile", "Account");
		}

        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        
        public IActionResult AccessDenied()
        {
            return View();
        }

        private int GetUserId()
        {
            string userIdClaimValue = HttpContext.User.Claims
                .Where(claim => claim.Type == ClaimTypes.NameIdentifier)
                .Select(claim => claim.Value)
                .FirstOrDefault();


            return Convert.ToInt32(userIdClaimValue);
        }

    }

}
