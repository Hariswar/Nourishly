using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models;

[Table("fund")]
public class Fund
{
    [Key]
    [Column("fund_id")]
    public int FundId { get; set; }
    
    [Column("meal_swipes")]
    public int MealSwipes { get; set; }
    
    [Column("dbds")]
    public decimal Dbds { get; set; }
}
