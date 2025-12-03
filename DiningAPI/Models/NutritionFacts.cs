using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DiningAPI.Models;

[Table("nutrition")]
public class Nutrition
{
    [Key]
    [Column("nutrition_id")]
    public int NutritionId { get; set; }
    
    [Column("calories")]
    public int? Calories { get; set; }
    
    [Column("protein")]
    public decimal? Protein { get; set; }
    
    [Column("fat")]
    public decimal? Fat { get; set; }
    
    [Column("carbs")]
    public decimal? Carbs { get; set; }
    
    [JsonIgnore]
    public List<MenuItem> MenuItems { get; set; } = new();
}