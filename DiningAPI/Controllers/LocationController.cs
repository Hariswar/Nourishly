using Microsoft.AspNetCore.Mvc;
using MediatR;
using DiningAPI.Features.Locations.Queries;
using DiningAPI.Features.Locations.Commands;

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

    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetLocationById), new { locationId = ((dynamic)result).LocationId }, result);
    }

    [HttpPut("{locationId:int}")]
    public async Task<IActionResult> UpdateLocation(int locationId, [FromBody] UpdateLocationCommand command)
    {
        var updateCommand = command with { LocationId = locationId };
        var result = await _mediator.Send(updateCommand);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet("{locationId:int}/menu")]
    public async Task<IActionResult> GetMenuByLocation(int locationId)
    {
        var result = await _mediator.Send(new GetMenusByLocationQuery(locationId));
        return result != null ? Ok(result) : NotFound();
    }
}