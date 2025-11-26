using MediatR;

namespace DiningAPI.Features.Locations.Queries;

public record GetMenusByLocationQuery(int LocationId) : IRequest<object?>;