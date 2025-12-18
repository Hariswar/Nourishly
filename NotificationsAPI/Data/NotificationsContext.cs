using Microsoft.EntityFrameworkCore;
using NotificationsAPI.Models;

namespace NotificationsAPI.Data;

public class NotificationsContext : DbContext
{
    public NotificationsContext(DbContextOptions<NotificationsContext> options) : base(options) { }
    
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notification>()
            .HasMany(n => n.Messages)
            .WithMany(m => m.Notifications)
            .UsingEntity("notification_message_map",
                l => l.HasOne(typeof(Message)).WithMany().HasForeignKey("message_id"),
                r => r.HasOne(typeof(Notification)).WithMany().HasForeignKey("notification_id"));
    }
}
