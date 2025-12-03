using MediatR;
using DiningAPI.Models;

namespace DiningAPI.Features.Menus.Queries;

public record GetAllMenuItemsQuery : IRequest<IEnumerable<MenuItem>>;
