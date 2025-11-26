namespace DiningAPI.Services;

public interface IMenuService
{
    Task<object?> GetMenuItemsAsync(int menuId);
    Task<object?> GetMenuItemDetailsAsync(int itemId);
}