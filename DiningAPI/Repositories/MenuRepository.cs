using Microsoft.EntityFrameworkCore;
using DiningAPI.Data;
using DiningAPI.Models;

namespace DiningAPI.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly DiningContext _context;

    public MenuRepository(DiningContext context)
    {
        _context = context;
    }

    public async Task<bool> MenuExistsAsync(int menuId)
    {
        return await _context.Menus.AnyAsync(m => m.MenuId == menuId);
    }

    public async Task<IEnumerable<MenuItem>> GetMenuItemsAsync(int menuId)
    {
        return await _context.Menus
            .Where(m => m.MenuId == menuId)
            .SelectMany(m => m.MenuItems)
            .Include(mi => mi.Nutritions)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetMenuItemByIdAsync(int itemId)
    {
        return await _context.MenuItems
            .Include(mi => mi.Nutritions)
            .FirstOrDefaultAsync(mi => mi.ItemId == itemId);
    }

    public async Task<bool> MenuItemExistsAsync(int itemId)
    {
        return await _context.MenuItems.AnyAsync(mi => mi.ItemId == itemId);
    }

    public async Task<MenuItem> CreateMenuItemAsync(int menuId, string name, string? description, decimal price, int? calories, decimal? protein, decimal? fat, decimal? carbs)
    {
        var menuItem = new MenuItem { Name = name, Description = description, Price = price };
        _context.MenuItems.Add(menuItem);
        await _context.SaveChangesAsync();

        // Create nutrition
        var nutrition = new Nutrition { Calories = calories, Protein = protein, Fat = fat, Carbs = carbs };
        _context.Nutritions.Add(nutrition);
        await _context.SaveChangesAsync();

        // Link nutrition to item
        menuItem.Nutritions.Add(nutrition);
        
        // Link item to menu
        var menu = await _context.Menus.Include(m => m.MenuItems).FirstAsync(m => m.MenuId == menuId);
        menu.MenuItems.Add(menuItem);
        
        await _context.SaveChangesAsync();
        return menuItem;
    }

    public async Task<MenuItem?> UpdateMenuItemAsync(int menuId, int itemId, string name, string? description, decimal price, int? calories, decimal? protein, decimal? fat, decimal? carbs)
    {
        var menuItem = await _context.MenuItems
            .Include(mi => mi.Nutritions)
            .Include(mi => mi.Menus)
            .FirstOrDefaultAsync(mi => mi.ItemId == itemId && mi.Menus.Any(m => m.MenuId == menuId));
            
        if (menuItem != null)
        {
            menuItem.Name = name;
            menuItem.Description = description;
            menuItem.Price = price;
            
            var nutrition = menuItem.Nutritions.FirstOrDefault();
            if (nutrition != null)
            {
                nutrition.Calories = calories;
                nutrition.Protein = protein;
                nutrition.Fat = fat;
                nutrition.Carbs = carbs;
            }
            
            await _context.SaveChangesAsync();
        }
        
        return menuItem;
    }
}