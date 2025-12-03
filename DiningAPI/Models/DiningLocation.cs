using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiningAPI.Models;

[Table("locations")]
public class Location
{
    [Key]
    [Column("location_id")]
    public int LocationId { get; set; }
    
    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("address")]
    public string? Address { get; set; }
    
    [Column("view_count")]
    public int ViewCount { get; set; } = 0;
    
    [JsonIgnore]
    public List<Menu> Menus { get; set; } = new();
}