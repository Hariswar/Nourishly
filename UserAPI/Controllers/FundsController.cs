using Microsoft.AspNetCore.Mvc;
using MediatR;
using UserAPI.Features.Funds.Queries;

namespace UserAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FundsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FundsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserFunds(int userId)
    {
        var result = await _mediator.Send(new GetUserFundsQuery(userId));
        return result != null ? Ok(result) : NotFound(new { message = "Funds not found" });
    }
}
