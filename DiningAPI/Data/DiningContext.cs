using Microsoft.EntityFrameworkCore;
using DiningAPI.Models;

namespace DiningAPI.Data;

public class DiningContext : DbContext
{
    public DiningContext(DbContextOptions<DiningContext> options) : base(options) { }
    
    public DbSet<Location> Locations { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Nutrition> Nutritions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Location-Menu many-to-many
        modelBuilder.Entity<Location>()
            .HasMany(l => l.Menus)
            .WithMany(m => m.Locations)
            .UsingEntity("location_menu_map",
                l => l.HasOne(typeof(Menu)).WithMany().HasForeignKey("menu_id"),
                r => r.HasOne(typeof(Location)).WithMany().HasForeignKey("location_id"));
        
        // Menu-MenuItem many-to-many
        modelBuilder.Entity<Menu>()
            .HasMany(m => m.MenuItems)
            .WithMany(mi => mi.Menus)
            .UsingEntity("menu_item_map",
                l => l.HasOne(typeof(MenuItem)).WithMany().HasForeignKey("item_id"),
                r => r.HasOne(typeof(Menu)).WithMany().HasForeignKey("menu_id"));
        
        // MenuItem-Nutrition many-to-many
        modelBuilder.Entity<MenuItem>()
            .HasMany(mi => mi.Nutritions)
            .WithMany(n => n.MenuItems)
            .UsingEntity("item_nutrition_map",
                l => l.HasOne(typeof(Nutrition)).WithMany().HasForeignKey("nutrition_id"),
                r => r.HasOne(typeof(MenuItem)).WithMany().HasForeignKey("item_id"));
    }
}