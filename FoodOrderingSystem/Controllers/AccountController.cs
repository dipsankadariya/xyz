using FoodOrderingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthServices _authServices;
        public AccountController(AuthServices authServices)
        {
            _authServices = authServices;
        }

       
        public IActionResult Login()
        {
            var userId = HttpContext.Session.GetInt32("UserId"); 
            var role = HttpContext.Session.GetString("Role");

            if (userId != null)
            {
                if (role == "Admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _authServices.Login(username, password);

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                if (user.Role == "Admin")
                    return RedirectToAction("Index", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Login";
            return View();
        }

        public IActionResult Register()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            var success = _authServices.Register(username, password, email);
            if (success)
                return RedirectToAction("Login");

            ViewBag.Error = "Registration failed!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
