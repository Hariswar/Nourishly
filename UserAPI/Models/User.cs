using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models;

// Details for user information
public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }
    
    [Column("first_name")]
    public string? FirstName { get; set; }
    
    [Column("last_name")]
    public string? LastName { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
}
