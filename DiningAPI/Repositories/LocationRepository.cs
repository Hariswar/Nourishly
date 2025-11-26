using Microsoft.EntityFrameworkCore;
using DiningAPI.Data;
using DiningAPI.Models;

namespace DiningAPI.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly DiningContext _context;

    public LocationRepository(DiningContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
    {
        return await _context.Locations.ToListAsync();
    }

    public async Task<Location?> GetLocationByIdAsync(int locationId)
    {
        return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
    }

    public async Task<bool> LocationExistsAsync(int locationId)
    {
        return await _context.Locations.AnyAsync(l => l.LocationId == locationId);
    }

    public async Task<IEnumerable<Menu>> GetMenusByLocationAsync(int locationId)
    {
        return await _context.Locations
            .Where(l => l.LocationId == locationId)
            .SelectMany(l => l.Menus)
            .ToListAsync();
    }
}