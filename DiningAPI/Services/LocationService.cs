using DiningAPI.Repositories;

namespace DiningAPI.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _locationRepository;

    public LocationService(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<object> GetAllLocationsAsync()
    {
        var locations = await _locationRepository.GetAllLocationsAsync();
        return locations.Select(l => new { l.LocationId, l.Name, l.Address });
    }

    public async Task<object?> GetLocationByIdAsync(int locationId)
    {
        var location = await _locationRepository.GetLocationByIdAsync(locationId);
        return location != null ? new { location.LocationId, location.Name, location.Address } : null;
    }

    public async Task<object?> GetMenusByLocationAsync(int locationId)
    {
        var locationExists = await _locationRepository.LocationExistsAsync(locationId);
        if (!locationExists) return null;

        var menus = await _locationRepository.GetMenusByLocationAsync(locationId);
        return menus.Select(m => new { m.MenuId, m.Name, m.Description });
    }
}