using MediatR;
using DiningAPI.Events;

namespace DiningAPI.Events.Handlers;

public class LocationAccessedEventHandler : INotificationHandler<LocationAccessedEvent>
{
    private readonly ILogger<LocationAccessedEventHandler> _logger;

    public LocationAccessedEventHandler(ILogger<LocationAccessedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LocationAccessedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Location {LocationId} ({LocationName}) was accessed at {OccurredOn}", 
            notification.LocationId, notification.LocationName, notification.OccurredOn);
        
        // Add analytics, caching, or other side effects here
        return Task.CompletedTask;
    }
}