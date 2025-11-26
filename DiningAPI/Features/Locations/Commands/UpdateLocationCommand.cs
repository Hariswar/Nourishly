using MediatR;

namespace DiningAPI.Features.Locations.Commands;

public record UpdateLocationCommand(int LocationId, string Name, string? Address) : IRequest<object?>;