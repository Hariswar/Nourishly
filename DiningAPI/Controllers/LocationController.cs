using Microsoft.AspNetCore.Mvc;
using MediatR;
using DiningAPI.Features.Locations.Queries;

namespace DiningAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        var result = await _mediator.Send(new GetAllLocationsQuery());
        return Ok(result);
    }

    [HttpGet("{locationId:int}")]
    public async Task<IActionResult> GetLocationById(int locationId)
    {
        var result = await _mediator.Send(new GetLocationByIdQuery(locationId));
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("{locationId:int}/menu")]
    public async Task<IActionResult> GetMenuByLocation(int locationId)
    {
        var result = await _mediator.Send(new GetMenusByLocationQuery(locationId));
        return result != null ? Ok(result) : NotFound();
    }
}