using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiningAPI.Models;

[Table("menu")]
public class Menu
{
    [Key]
    [Column("menu_id")]
    public int MenuId { get; set; }
    
    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("description")]
    public string? Description { get; set; }
    
    public List<Location> Locations { get; set; } = new();
    [JsonIgnore]
    public List<MenuItem> MenuItems { get; set; } = new();
}