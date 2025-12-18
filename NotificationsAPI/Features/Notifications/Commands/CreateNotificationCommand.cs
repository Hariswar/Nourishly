using MediatR;

namespace NotificationsAPI.Features.Notifications.Commands;

public record CreateNotificationCommand(int UserId, string Type, string Content) : IRequest<object>;
public record MarkAsReadCommand(int NotificationId) : IRequest<bool>;
