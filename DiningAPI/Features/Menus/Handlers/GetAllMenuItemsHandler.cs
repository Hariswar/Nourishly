using MediatR;
using DiningAPI.Features.Menus.Queries;
using DiningAPI.Models;
using DiningAPI.Repositories;

namespace DiningAPI.Features.Menus.Handlers;

public class GetAllMenuItemsHandler : IRequestHandler<GetAllMenuItemsQuery, IEnumerable<MenuItem>>
{
    private readonly IMenuRepository _repository;

    public GetAllMenuItemsHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MenuItem>> Handle(GetAllMenuItemsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllMenuItemsAsync();
    }
}
