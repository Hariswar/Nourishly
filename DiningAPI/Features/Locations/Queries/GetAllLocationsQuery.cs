using MediatR;

namespace DiningAPI.Features.Locations.Queries;

public record GetAllLocationsQuery : IRequest<IEnumerable<object>>;