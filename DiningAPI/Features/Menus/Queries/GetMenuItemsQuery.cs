using MediatR;

namespace DiningAPI.Features.Menus.Queries;

public record GetMenuItemsQuery(int MenuId) : IRequest<object?>;