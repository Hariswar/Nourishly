using MediatR;
using DiningAPI.Features.Locations.Queries;
using DiningAPI.Repositories;
using DiningAPI.Infrastructure;
using DiningAPI.Events;

namespace DiningAPI.Features.Locations.Handlers;

public class GetAllLocationsHandler : IRequestHandler<GetAllLocationsQuery, IEnumerable<object>>
{
    private readonly ILocationRepository _repository;

    public GetAllLocationsHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<object>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
    {
        var locations = await _repository.GetAllLocationsAsync();
        return locations.Select(l => new { l.LocationId, l.Name, l.Address, l.ViewCount });
    }
}

public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, object?>
{
    private readonly ILocationRepository _repository;
    private readonly IMediator _mediator;

    public GetLocationByIdHandler(ILocationRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<object?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _repository.GetLocationByIdAsync(request.LocationId);
        
        if (location != null)
        {
            // Handle event locally (increment counter)
            var locationEvent = new LocationAccessedEvent(location.LocationId, location.Name, DateTime.UtcNow);
            await _mediator.Publish(locationEvent, cancellationToken);
        }
        
        return location != null ? new { location.LocationId, location.Name, location.Address, location.ViewCount } : null;
    }
}

public class GetMenusByLocationHandler : IRequestHandler<GetMenusByLocationQuery, object?>
{
    private readonly ILocationRepository _repository;

    public GetMenusByLocationHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(GetMenusByLocationQuery request, CancellationToken cancellationToken)
    {
        var locationExists = await _repository.LocationExistsAsync(request.LocationId);
        if (!locationExists) return null;

        var menus = await _repository.GetMenusByLocationAsync(request.LocationId);
        return menus.Select(m => new { m.MenuId, m.Name, m.Description });
    }
}