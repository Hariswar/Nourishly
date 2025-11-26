using DiningAPI.Models;

namespace DiningAPI.Repositories;

public interface IMenuRepository
{
    Task<bool> MenuExistsAsync(int menuId);
    Task<IEnumerable<MenuItem>> GetMenuItemsAsync(int menuId);
    Task<MenuItem?> GetMenuItemByIdAsync(int itemId);
    Task<bool> MenuItemExistsAsync(int itemId);
}