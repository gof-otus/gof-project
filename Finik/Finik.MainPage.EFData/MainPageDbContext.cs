using Microsoft.EntityFrameworkCore;
using Finik.MainPage.Core.Models;

namespace Finik.MainPage.EFData
{
    public class MainPageDbContext : DbContext
    {
        public MainPageDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<News> News { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}
