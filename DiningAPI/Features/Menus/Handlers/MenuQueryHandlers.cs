using MediatR;
using DiningAPI.Features.Menus.Queries;
using DiningAPI.Repositories;

namespace DiningAPI.Features.Menus.Handlers;

public class GetMenuItemsHandler : IRequestHandler<GetMenuItemsQuery, object?>
{
    private readonly IMenuRepository _repository;

    public GetMenuItemsHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
    {
        var menuExists = await _repository.MenuExistsAsync(request.MenuId);
        if (!menuExists) return null;

        var items = await _repository.GetMenuItemsAsync(request.MenuId);
        return items.Select(mi => new {
            mi.ItemId,
            mi.Name,
            mi.Description,
            mi.Price,
            Nutrition = mi.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        });
    }
}

public class GetMenuItemDetailsHandler : IRequestHandler<GetMenuItemDetailsQuery, object?>
{
    private readonly IMenuRepository _repository;

    public GetMenuItemDetailsHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?> Handle(GetMenuItemDetailsQuery request, CancellationToken cancellationToken)
    {
        var itemExists = await _repository.MenuItemExistsAsync(request.ItemId);
        if (!itemExists) return null;

        var item = await _repository.GetMenuItemByIdAsync(request.ItemId);
        
        return item != null ? new {
            item.ItemId,
            item.Name,
            item.Description,
            item.Price,
            Nutrition = item.Nutritions.Select(n => new {
                n.NutritionId,
                n.Calories,
                n.Protein,
                n.Fat,
                n.Carbs
            })
        } : null;
    }
}