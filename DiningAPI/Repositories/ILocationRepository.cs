using DiningAPI.Models;

namespace DiningAPI.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocationsAsync();
    Task<Location?> GetLocationByIdAsync(int locationId);
    Task<bool> LocationExistsAsync(int locationId);
    Task<IEnumerable<Menu>> GetMenusByLocationAsync(int locationId);
}