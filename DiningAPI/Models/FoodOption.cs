using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiningAPI.Models;

[Table("menu_items")]
public class MenuItem
{
    [Key]
    [Column("item_id")]
    public int ItemId { get; set; }
    
    [Required]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Column("description")]
    public string? Description { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    public List<Menu> Menus { get; set; } = new();
    public List<Nutrition> Nutritions { get; set; } = new();
}