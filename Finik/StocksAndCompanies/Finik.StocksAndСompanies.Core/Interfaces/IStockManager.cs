using Finik.StockAndCompany.Core.Models;

namespace Finik.StockAndCompany.Core.Interfaces
{
    public interface IStockManager
    {
        public Task<int> CreateStock(Stock stock);
        public Task<Stock?> GetStock(int id);
        public Task<List<Stock>> GetAllStocks();
        public Task<List<Stock>?> GetCompanyStocks(int companyId);
    }
}