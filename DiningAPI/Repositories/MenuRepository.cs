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
}