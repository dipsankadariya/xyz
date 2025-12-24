using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Repositories.Interfaces
{
    public interface ICartRepository
    {
        List<CartItem> GetCartItems(int userId);

        void AddToCart(int userId, int itemId, int quantity);

        void UpdateCartItem(int userId, int itemId, int quantity);

        void RemoveFromCart(int userId, int itemId);
        void clearCart(int userId);


    }
}
