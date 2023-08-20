using Finik.Data.Configurations;
using Finik.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data;

public class FinikDbContext : DbContext
{
    public FinikDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    public DbSet<News> News { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NewsDbConfiguration());
        modelBuilder.ApplyConfiguration(new UserDbConfiguration());
    }
}