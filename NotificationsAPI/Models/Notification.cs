using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NotificationsAPI.Models;

[Table("notification")]
public class Notification
{
    [Key]
    [Column("notification_id")]
    public int NotificationId { get; set; }
    
    [Required]
    [Column("type")]
    public string Type { get; set; } = string.Empty;
    
    [Required]
    [Column("status")]
    public string Status { get; set; } = "unread";
    
    [JsonIgnore]
    public List<Message> Messages { get; set; } = new();
}

[Table("message")]
public class Message
{
    [Key]
    [Column("message_id")]
    public int MessageId { get; set; }
    
    [Required]
    [Column("content")]
    public string Content { get; set; } = string.Empty;
    
    [JsonIgnore]
    public List<Notification> Notifications { get; set; } = new();
}
