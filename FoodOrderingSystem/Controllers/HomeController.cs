using FoodOrderingSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMenuRepository _menuRepository;

        public HomeController(IMenuRepository menuRepository) { 
            _menuRepository = menuRepository;
        }

        public IActionResult Index() { 
        
        var menuItems= _menuRepository.GetAllMenuItems(); 
            return View(menuItems);
        }
    }
}
