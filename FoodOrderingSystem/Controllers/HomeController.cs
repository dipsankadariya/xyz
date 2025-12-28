using Microsoft.AspNetCore.Mvc;
using FoodOrderingSystem.Repositories.Interfaces;

namespace FoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuRepository _menuRepository;

        public HomeController(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        
        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult Foods()
        {
            var menuItems = _menuRepository.GetAllMenuItems();
            return View(menuItems);
        }
    }
}