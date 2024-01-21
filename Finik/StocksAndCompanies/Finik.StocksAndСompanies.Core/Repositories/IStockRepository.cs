using Finik.StockAndCompany.Core.Models;

namespace Finik.StockAndCompany.Core.Repositories
{
    public interface IStockRepository
    {
        public Task<int> CreateStock(Stock stock);
        public Task<Stock?> GetStock(int id);
        public Task DeleteStock(int id);
        public Task<List<Stock>> GetAllStocks();
        public Task<List<Stock>?> GetCompanyStocks(int companyId);
    }
}