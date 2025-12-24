using FoodOrderingSystem.Models;
using FoodOrderingSystem.Repositories.Interfaces;
using Microsoft.Data.SqlClient;

namespace FoodOrderingSystem.Repositories.Implementations
{
    public class MenuRepository : IMenuRepository
    {
        private readonly string connectionstring;

        public MenuRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public List<MenuItem> GetAllMenuItems()
        {
            var items = new List<MenuItem>();

            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                string query = "SELECT * FROM MenuItems WHERE QuantityAvailable > 0";

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new MenuItem
                        {
                            ItemId = (int)reader["ItemId"],
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = (decimal)reader["Price"],
                            Category = reader["Category"].ToString(),
                            QuantityAvailable = (int)reader["QuantityAvailable"]
                        });
                    }
                }
            }

            return items;
        }


        public MenuItem GetMenuItemById(int id)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "SELECT * FROM MenuItems WHERE ItemId=@ItemId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MenuItem
                            {
                                ItemId = (int)reader["ItemId"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Price = (decimal)reader["Price"],
                                Category = reader["Category"].ToString(),
                                QuantityAvailable = (int)reader["QuantityAvailable"]
                            };
                        }
                    }
                }
            }

            return null;
        }



        public void AddMenuItem(MenuItem item)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "INSERT INTO MenuItems (Name, Description, Price, Category, QuantityAvailable) VALUES (@Name, @Description, @Price, @Category, @QuantityAvailable)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@Category", item.Category);
                    command.Parameters.AddWithValue("@QuantityAvailable", item.QuantityAvailable);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMenuItem(int id)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();

                string query = "DELETE FROM MenuItems WHERE ItemId=@id";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMenuItem(MenuItem item)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string query = "UPDATE MenuItems SET Name=@Name, Description=@Description, Price=@Price, Category=@Category, QuantityAvailable=@QuantityAvailable WHERE ItemId=@ItemId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemId", item.ItemId);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Description", item.Description);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.Parameters.AddWithValue("@Category", item.Category);
                    command.Parameters.AddWithValue("@QuantityAvailable", item.QuantityAvailable);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
