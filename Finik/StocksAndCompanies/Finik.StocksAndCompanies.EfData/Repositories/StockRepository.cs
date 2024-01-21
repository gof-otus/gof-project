using Finik.StockAndCompany.Core.Models;
using Finik.StockAndCompany.Core.Repositories;

namespace Finik.StocksAndCompanies.EfData.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockAndCompaniesDbContext _context;

        public StockRepository(StockAndCompaniesDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateStock(Stock stock)
        { 
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock.Id;
        }

        public async Task<Stock?> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock;
        }

        public async Task DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Stock>> GetAllStocks()
        {
            return Task.Run(() => _context.Stocks.ToList());
        }

        public async Task<List<Stock>?> GetCompanyStocks(int companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            return company?.Stocks?.ToList();
        }
    }
}
