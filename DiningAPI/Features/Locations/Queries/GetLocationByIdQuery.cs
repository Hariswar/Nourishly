using MediatR;

namespace DiningAPI.Features.Locations.Queries;

public record GetLocationByIdQuery(int LocationId) : IRequest<object?>;