namespace DiningAPI.Services;

public interface ILocationService
{
    Task<object> GetAllLocationsAsync();
    Task<object?> GetLocationByIdAsync(int locationId);
    Task<object?> GetMenusByLocationAsync(int locationId);
}