using DiningAPI.Repositories;

namespace DiningAPI.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<object?> GetMenuItemsAsync(int menuId)
    {
        var menuExists = await _menuRepository.MenuExistsAsync(menuId);
        if (!menuExists) return null;

        var items = await _menuRepository.GetMenuItemsAsync(menuId);
        return items.Select(mi => new {
            mi.ItemId,
            mi.Name,
            mi.Description,
            mi.Price,
            Nutrition = mi.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        });
    }

    public async Task<object?> GetMenuItemDetailsAsync(int itemId)
    {
        var itemExists = await _menuRepository.MenuItemExistsAsync(itemId);
        if (!itemExists) return null;

        var item = await _menuRepository.GetMenuItemByIdAsync(itemId);
        return item != null ? new {
            item.ItemId,
            item.Name,
            item.Description,
            item.Price,
            Nutrition = item.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        } : null;
    }
}