using DiningAPI.Models;

namespace DiningAPI.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllLocationsAsync();
    Task<Location?> GetLocationByIdAsync(int locationId);
    Task<bool> LocationExistsAsync(int locationId);
    Task<IEnumerable<Menu>> GetMenusByLocationAsync(int locationId);
    Task IncrementViewCountAsync(int locationId);
    Task<Location> CreateLocationAsync(string name, string? address);
    Task<Location?> UpdateLocationAsync(int locationId, string name, string? address);
}