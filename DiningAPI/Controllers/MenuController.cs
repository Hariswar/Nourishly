using Microsoft.AspNetCore.Mvc;
using MediatR;
using DiningAPI.Features.Menus.Queries;

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
}