using MediatR;
using DiningAPI.Events;
using DiningAPI.Repositories;

namespace DiningAPI.Events.Handlers;

public class LocationViewedEventHandler : INotificationHandler<LocationAccessedEvent>
{
    private readonly ILocationRepository _locationRepository;
    private readonly ILogger<LocationViewedEventHandler> _logger;

    public LocationViewedEventHandler(ILocationRepository locationRepository, ILogger<LocationViewedEventHandler> logger)
    {
        _locationRepository = locationRepository;
        _logger = logger;
    }

    public async Task Handle(LocationAccessedEvent notification, CancellationToken cancellationToken)
    {
        await _locationRepository.IncrementViewCountAsync(notification.LocationId);
        _logger.LogInformation("Incremented view count for location {LocationId}", notification.LocationId);
    }
}