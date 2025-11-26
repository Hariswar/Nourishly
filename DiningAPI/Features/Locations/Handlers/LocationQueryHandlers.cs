using MediatR;
using DiningAPI.Features.Locations.Queries;
using DiningAPI.Repositories;

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
        return locations.Select(l => new { l.LocationId, l.Name, l.Address });
    }
}

public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, object?>
{
    private readonly ILocationRepository _repository;

    public GetLocationByIdHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var location = await _repository.GetLocationByIdAsync(request.LocationId);
        return location != null ? new { location.LocationId, location.Name, location.Address } : null;
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