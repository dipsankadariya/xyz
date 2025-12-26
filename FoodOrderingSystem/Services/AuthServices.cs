using FoodOrderingSystem.Models;
using FoodOrderingSystem.Repositories.Interfaces;
using BCrypt.Net;
namespace FoodOrderingSystem.Services
{
    public class AuthServices
    {
        private readonly IUserRepository _userRepository;

        public AuthServices(IUserRepository userRepository) {

            _userRepository = userRepository;
        }


        public bool Register(string username, string password, string email, string role = "Customer")
        {
            if (_userRepository.UserExists(username)) {
                return false;
               }
            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email,
                Role = role
            };

            _userRepository.AddUser(user);
            return true;

        }


        public User Login(string username, string password)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

    }

}


