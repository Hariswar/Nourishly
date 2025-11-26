using Microsoft.AspNetCore.Mvc;
using MediatR;
using DiningAPI.Features.Menus.Queries;

namespace DiningAPI.Controllers;

[ApiController]
[Route("menu-items")]
public class MenuItemController : ControllerBase
{
    private readonly IMediator _mediator;

    public MenuItemController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{itemId:int}")]
    public async Task<IActionResult> GetMenuItemDetails(int itemId)
    {
        var result = await _mediator.Send(new GetMenuItemDetailsQuery(itemId));
        return result != null ? Ok(result) : NotFound();
    }
}