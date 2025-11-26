using MediatR;

namespace DiningAPI.Features.Locations.Commands;

public record CreateLocationCommand(string Name, string? Address) : IRequest<object>;