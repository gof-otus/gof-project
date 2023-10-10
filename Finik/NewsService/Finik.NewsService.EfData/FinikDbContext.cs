using Finik.NewsService.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.Data;

public class FinikDbContext : DbContext
{
    public FinikDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
    public DbSet<News> News { get; set; }
}