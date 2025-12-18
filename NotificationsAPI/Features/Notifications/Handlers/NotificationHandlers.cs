using MediatR;
using Microsoft.EntityFrameworkCore;
using NotificationsAPI.Data;
using NotificationsAPI.Models;
using NotificationsAPI.Features.Notifications.Queries;
using NotificationsAPI.Features.Notifications.Commands;

namespace NotificationsAPI.Features.Notifications.Handlers;

public class GetUserNotificationsHandler : IRequestHandler<GetUserNotificationsQuery, IEnumerable<object>>
{
    private readonly NotificationsContext _context;

    public GetUserNotificationsHandler(NotificationsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notificationIds = await _context.Database
            .SqlQuery<int>($"SELECT notification_id FROM notification_user_map WHERE user_id = {request.UserId}")
            .ToListAsync(cancellationToken);
        
        var notifications = await _context.Notifications
            .Include(n => n.Messages)
            .Where(n => notificationIds.Contains(n.NotificationId))
            .ToListAsync(cancellationToken);
        
        return notifications.Select(n => new { 
            n.NotificationId, 
            n.Type, 
            n.Status, 
            Messages = n.Messages.Select(m => new { m.MessageId, m.Content })
        });
    }
}

public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, object>
{
    private readonly NotificationsContext _context;

    public CreateNotificationHandler(NotificationsContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var message = new Message { Content = request.Content };
        var notification = new Notification { Type = request.Type };
        notification.Messages.Add(message);
        
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);
        
        await _context.Database.ExecuteSqlAsync(
            $"INSERT INTO notification_user_map (notification_id, user_id) VALUES ({notification.NotificationId}, {request.UserId})",
            cancellationToken);
        
        return new { notification.NotificationId, notification.Type, notification.Status, Message = new { message.MessageId, message.Content } };
    }
}

public class MarkAsReadHandler : IRequestHandler<MarkAsReadCommand, bool>
{
    private readonly NotificationsContext _context;

    public MarkAsReadHandler(NotificationsContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _context.Notifications.FindAsync(new object[] { request.NotificationId }, cancellationToken);
        if (notification == null) return false;
        
        notification.Status = "read";
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
