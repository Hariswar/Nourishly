using DiningAPI.Models;

namespace DiningAPI.Repositories;

public interface IMenuRepository
{
    Task<bool> MenuExistsAsync(int menuId);
    Task<IEnumerable<MenuItem>> GetMenuItemsAsync(int menuId);
    Task<MenuItem?> GetMenuItemByIdAsync(int itemId);
    Task<bool> MenuItemExistsAsync(int itemId);
    Task<MenuItem> CreateMenuItemAsync(int menuId, string name, string? description, decimal price, int? calories, decimal? protein, decimal? fat, decimal? carbs);
    Task<MenuItem?> UpdateMenuItemAsync(int menuId, int itemId, string name, string? description, decimal price, int? calories, decimal? protein, decimal? fat, decimal? carbs);
}