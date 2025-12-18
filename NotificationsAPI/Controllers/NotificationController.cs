using Microsoft.AspNetCore.Mvc;
using MediatR;
using NotificationsAPI.Features.Notifications.Queries;
using NotificationsAPI.Features.Notifications.Commands;

namespace NotificationsAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("user/{userId:int}")]
    public async Task<IActionResult> GetUserNotifications(int userId)
    {
        var result = await _mediator.Send(new GetUserNotificationsQuery(userId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserNotifications), new { userId = command.UserId }, result);
    }

    [HttpPatch("{notificationId:int}/read")]
    public async Task<IActionResult> MarkAsRead(int notificationId)
    {
        var result = await _mediator.Send(new MarkAsReadCommand(notificationId));
        return result ? NoContent() : NotFound();
    }
}
