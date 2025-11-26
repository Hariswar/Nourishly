using MediatR;

namespace DiningAPI.Features.Menus.Commands;

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