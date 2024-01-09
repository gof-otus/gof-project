using Finik.StockAndCompany.Core.Models;
using Finik.StocksAndCompanies.EfData.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Finik.StocksAndCompanies.EfData;

public class StockAndCompaniesDbContext : DbContext
{
    public StockAndCompaniesDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StockConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Stock> Stocks { get; set; }
}