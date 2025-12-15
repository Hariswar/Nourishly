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
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.Email).HasColumnName("email");
        });
        
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.ToTable("auth");
            entity.HasKey(e => e.AuthId);
            entity.Property(e => e.AuthId).HasColumnName("auth_id");
            entity.Property(e => e.Username).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password_hash");
        });
    }
}
