using FoodOrderingSystem.Models;
namespace FoodOrderingSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void AddUser(User user);
        bool UserExists(string username);
    }

}
