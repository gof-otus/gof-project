using Finik.StockAndCompany.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finik.StocksAndCompanies.EfData.Configuration
{
    internal class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(stock => stock.Id);
            builder.HasOne(stock => stock.Company).WithMany(company => company.Stocks).OnDelete(DeleteBehavior.Cascade); ;
        }
    }
}
