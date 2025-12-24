using FoodOrderingSystem.Models;
using FoodOrderingSystem.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace FoodOrderingSystem.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly string connectionstring;

        public CartRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        public void AddToCart(int userId, int itemId, int quantity)
        {
          

            using(var connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query =
            }
        }

        public void clearCart(int userId)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetCartItems(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveFromCart(int userId, int itemId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCartItem(int userId, int itemId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
