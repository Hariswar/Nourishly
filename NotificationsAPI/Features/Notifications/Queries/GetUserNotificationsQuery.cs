using MediatR;

namespace NotificationsAPI.Features.Notifications.Queries;

public record GetUserNotificationsQuery(int UserId) : IRequest<IEnumerable<object>>;
