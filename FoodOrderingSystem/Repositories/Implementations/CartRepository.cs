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
                //first check if the item already in cart or not exists or not..
                //checking query
                string query = "SELECT COUNT(*) FROM  Carts WHERE UserId= @UserId  and ItemId= @ItemId";
                bool exists = false;
                using(var command = new SqlCommand(query, connection) )
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    //execute the command and checks whether there is at least one row matching the query.
                    //ff yes, exists becomes true; if not, it becomes false.
                    exists = (int)command.ExecuteScalar() >0;

                }
                if (exists)
                {
                    //update query
                    string updatequery = "UPDATE Carts Set Quantity= Quantity+@Quantity WHERE UserId=@UserId AND ItemId=@ItemId ";

                    using (var command= new SqlCommand(updatequery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ItemId", itemId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.ExecuteNonQuery(); 
                    }
                }
                else
                {
                    //add query
                    string insertQuery = "INSERT INTO Carts (UserId,ItemId, Quantity) VALUES (@UserId, @ItemId, @Quantity)";


                    using(var command= new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ItemId", itemId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.ExecuteNonQuery();
                    }
                }
            }

        }

        public void ClearCart(int userId)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                string query = "DELETE FROM Carts where UserId=@UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId",userId);
                    command.ExecuteNonQuery (); 
                }
            }
        }

        public List<CartItem> GetCartItems(int userId)
        {
            var cart = new List<CartItem>();
            using (var connection = new SqlConnection(connectionstring))
            {

                connection.Open();

                string query = @"
                            SELECT c.CartId, c.ItemId, m.Name, m.Price, m.ImageUrl, c.Quantity 
                            FROM Carts c 
                            JOIN MenuItems m ON c.ItemId = m.ItemId 
                            WHERE c.UserId = @UserId";


                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            cart.Add(new CartItem
                            {
                                CartId = (int)reader["CartId"],
                                ItemId = (int)reader["ItemId"],
                                Name = reader["Name"].ToString(),
                                Price = (decimal)reader["Price"],
                                Quantity = (int)reader["Quantity"],
                                ImageUrl = reader["ImageUrl"]?.ToString()
                            });


                        }
                    }
                }
                return cart;
            }
        }

        public void RemoveFromCart(int userId, int itemId)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                string query = "DELETE FROM Carts WHERE UserId=@UserId AND ItemId=@ItemId";

                using (var command = new SqlCommand(query, connection)) { 
                
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCartItem(int userId, int itemId, int quantity)
        {
            using (var connection = new SqlConnection(connectionstring)) { 
            
            connection.Open();

                if (quantity <= 0)
                {
                    RemoveFromCart(userId, itemId);
                }
                else
                {
                    string query = "UPDATE Carts SET Quantity = @Quantity WHERE UserId = @UserId AND ItemId = @ItemId";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ItemId", itemId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
