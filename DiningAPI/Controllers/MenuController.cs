using Microsoft.AspNetCore.Mvc;
using MediatR;
using DiningAPI.Features.Menus.Queries;
using DiningAPI.Features.Menus.Commands;

namespace DiningAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{menuId:int}/items")]
    public async Task<IActionResult> GetMenuItems(int menuId)
    {
        var result = await _mediator.Send(new GetMenuItemsQuery(menuId));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost("{menuId:int}/items")]
    public async Task<IActionResult> CreateMenuItem(int menuId, [FromBody] CreateMenuItemCommand command)
    {
        var createCommand = command with { MenuId = menuId };
        var result = await _mediator.Send(createCommand);
        return result != null ? CreatedAtAction(nameof(GetMenuItems), new { menuId }, result) : NotFound($"Menu with ID {menuId} not found");
    }

    [HttpPut("{menuId:int}/items/{itemId:int}")]
    public async Task<IActionResult> UpdateMenuItem(int menuId, int itemId, [FromBody] UpdateMenuItemCommand command)
    {
        var updateCommand = command with { MenuId = menuId, ItemId = itemId };
        var result = await _mediator.Send(updateCommand);
        return result != null ? Ok(result) : NotFound();
    }
}