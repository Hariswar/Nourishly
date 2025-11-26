using MediatR;
using DiningAPI.Features.Menus.Commands;
using DiningAPI.Repositories;

namespace DiningAPI.Features.Menus.Handlers;

public class CreateMenuItemHandler : IRequestHandler<CreateMenuItemCommand, object?>
{
    private readonly IMenuRepository _repository;

    public CreateMenuItemHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuExists = await _repository.MenuExistsAsync(request.MenuId);
        if (!menuExists) return null;

        var menuItem = await _repository.CreateMenuItemAsync(
            request.MenuId, request.Name, request.Description, request.Price,
            request.Calories, request.Protein, request.Fat, request.Carbs);

        return new {
            menuItem.ItemId,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price,
            Nutrition = menuItem.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        };
    }
}

public class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItemCommand, object?>
{
    private readonly IMenuRepository _repository;

    public UpdateMenuItemHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
    {
        var menuItem = await _repository.UpdateMenuItemAsync(
            request.MenuId, request.ItemId, request.Name, request.Description, request.Price,
            request.Calories, request.Protein, request.Fat, request.Carbs);

        return menuItem != null ? new {
            menuItem.ItemId,
            menuItem.Name,
            menuItem.Description,
            menuItem.Price,
            Nutrition = menuItem.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        } : null;
    }
}