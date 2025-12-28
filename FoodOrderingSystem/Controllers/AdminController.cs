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
        public IActionResult AddItem()
        {

            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }
            return View();

        }

        [HttpPost]
        public IActionResult AddItem(MenuItem item)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Amin")
            {
                return RedirectToAction("Login", "Account");
            }

            _menuRepository.AddMenuItem(item);
            return View(item);

        }

        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _menuRepository.GetMenuItemById(id);
            if (item == null)
            {
                return RedirectToAction("Index");
            }

            return View(item);
        }


        [HttpPost]
        public IActionResult EditItem(MenuItem item)
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("Login","Account");
            }
            _menuRepository.UpdateMenuItem(item);
               return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteItem(int id) {

            var role = HttpContext.Session.GetString("Role");
            if(role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }
            _menuRepository.DeleteMenuItem(id);
            return RedirectToAction("Index");
        }
    }
}
