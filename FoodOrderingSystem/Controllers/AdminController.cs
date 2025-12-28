using FoodOrderingSystem.Models;
using FoodOrderingSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public AdminController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            var items = _menuRepository.GetAllMenuItems();
            return View(items);
        }

        [HttpGet]
        public IActionResult AddMenuItem() 
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            return View(); 
        }

        [HttpPost]
        public IActionResult AddMenuItem(MenuItem item) 
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") 
                return RedirectToAction("Login", "Account");

            _menuRepository.AddMenuItem(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditMenuItem(int id) 
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            var item = _menuRepository.GetMenuItemById(id);
            if (item == null)
                return RedirectToAction("Index");

            return View(item);
        }

        [HttpPost]
        public IActionResult EditMenuItem(MenuItem item) 
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            _menuRepository.UpdateMenuItem(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteMenuItem(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
                return RedirectToAction("Login", "Account");

            _menuRepository.DeleteMenuItem(id);
            return RedirectToAction("Index");
        }
    }
}