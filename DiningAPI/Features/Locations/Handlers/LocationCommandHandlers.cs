using MediatR;
using DiningAPI.Features.Locations.Commands;
using DiningAPI.Repositories;

namespace DiningAPI.Features.Locations.Handlers;

public class CreateLocationHandler : IRequestHandler<CreateLocationCommand, object>
{
    private readonly ILocationRepository _repository;

    public CreateLocationHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _repository.CreateLocationAsync(request.Name, request.Address);
        return new { location.LocationId, location.Name, location.Address };
    }
}

public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, object?>
{
    private readonly ILocationRepository _repository;

    public UpdateLocationHandler(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = await _repository.UpdateLocationAsync(request.LocationId, request.Name, request.Address);
        return location != null ? new { location.LocationId, location.Name, location.Address } : null;
    }
}