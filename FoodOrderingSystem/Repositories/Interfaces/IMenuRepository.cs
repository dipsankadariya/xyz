using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Repositories.Interfaces
{
    public interface IMenuRepository
    {

        List<MenuItem> GetAllMenuItems();
        MenuItem GetMenuItemById(int id);

        void AddMenuItem(MenuItem item);

        void UpdateMenuItem(MenuItem item);

        void DeleteMenuItem(int id);
    }
}
