using MediatR;

namespace DiningAPI.Features.Menus.Commands;

// In order to update the menu items
public record UpdateMenuItemCommand(
    int MenuId,
    int ItemId,
    string Name,
    string? Description,
    decimal Price,
    int? Calories,
    decimal? Protein,
    decimal? Fat,
    decimal? Carbs
) : IRequest<object?>;