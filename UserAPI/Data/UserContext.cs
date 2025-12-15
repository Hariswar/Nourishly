using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Auth> Auths { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("\"User\"");
        modelBuilder.Entity<Auth>().ToTable("auth");
    }
}
