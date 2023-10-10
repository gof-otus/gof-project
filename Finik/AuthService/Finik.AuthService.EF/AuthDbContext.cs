using Finik.AuthService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.AuthService.EF;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    { 
        Database.EnsureCreated(); 
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, Name = "Administrator" },
            new Role() { Id = 2, Name = "Editor" },
            new Role() { Id = 3, Name = "Author" });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(u => u.Role);
            entity.HasIndex(u => u.Email).IsUnique();
        });
    }
}