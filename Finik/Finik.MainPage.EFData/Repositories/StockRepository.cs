using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;

namespace Finik.MainPage.EFData.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly MainPageDbContext _context;

        public StockRepository(MainPageDbContext mainPageDbContext)
        {
            _context = mainPageDbContext;
        }

        public async Task AddStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteStock(int id)
        //{
        //    var stock = await _context.Stocks.FindAsync(id);
        //    if (stock != null)
        //    {
        //        _context.Stocks.Remove(stock);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<Stock?> GetStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            return stock;
        }

        //public async Task UpdateStock(Stock stock)
        //{
        //    var stockOld = await _context.Stocks.FindAsync(stock.Id);
        //    if (stockOld != null)
        //    {
        //        _context.Stocks.Remove(stockOld);
        //    }
        //    await _context.Stocks.AddAsync(stock);
        //    await _context.SaveChangesAsync();
        //}
    }
}
