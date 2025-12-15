using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserAPI.Models;

public class Auth
{
    [Key]
    [Column("auth_id")]
    public int AuthId { get; set; }
    
    [Column("username")]
    public string? Username { get; set; }
    
    [Column("password_hash")]
    [JsonIgnore]
    public string? Password { get; set; }
}
