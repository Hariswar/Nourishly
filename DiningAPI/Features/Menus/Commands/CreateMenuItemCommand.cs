using MediatR;

namespace DiningAPI.Features.Menus.Commands;

public record CreateMenuItemCommand(
    int MenuId, 
    string Name, 
    string? Description, 
    decimal Price,
    int? Calories,
    decimal? Protein,
    decimal? Fat,
    decimal? Carbs
) : IRequest<object?>;