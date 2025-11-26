using MediatR;

namespace DiningAPI.Features.Menus.Queries;

public record GetMenuItemDetailsQuery(int ItemId) : IRequest<object?>;