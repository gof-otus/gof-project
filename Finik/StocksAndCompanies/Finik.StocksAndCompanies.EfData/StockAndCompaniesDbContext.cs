﻿using System.Reflection;
using Finik.StockAndCompany.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Finik.StocksAndCompanies.EfData;

public class StockAndCompaniesDbContext : DbContext
{
    public StockAndCompaniesDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Company> Companies { get; set; }
    public DbSet<Stock> Stocks { get; set; }
}