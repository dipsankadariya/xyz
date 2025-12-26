using FoodOrderingSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Controllers
{
    public class CartController : Controller
    {

        private readonly ICartRepository _cartRepository;
        private readonly IMenuRepository _menuRepository;

        public CartController(ICartRepository cartRepository, IMenuRepository menuRepository)
        {
            _cartRepository = cartRepository;
            _menuRepository = menuRepository;
        }

        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = _cartRepository.GetCartItems(userId.Value);

            decimal total = 0;

            foreach (var item in cart)
            {
                total = total + item.Subtotal;
            }

            ViewBag.Total = total;

            return View(cart);
        }


        public IActionResult Add(int itemId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _cartRepository.AddToCart(userId.Value, itemId, 1);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int itemId)
        {

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            _cartRepository.RemoveFromCart(userId.Value, itemId);
            return RedirectToAction("Index", "Home");
        }
    }

}