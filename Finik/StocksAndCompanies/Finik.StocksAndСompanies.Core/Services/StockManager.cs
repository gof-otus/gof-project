using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Models;
using Finik.StockAndCompany.Core.Repositories;

namespace Finik.StockAndCompany.Core.Services
{
    public class StockManager : IStockManager
    {
        private readonly IStockRepository _stockRepository;

        public StockManager(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public Task<int> CreateStock(Stock stock)
        {
            return _stockRepository.CreateStock(stock);
        }

        public Task<Stock?> GetStock(int id)
        {
            return _stockRepository.GetStock(id);
        }

        public Task<List<Stock>> GetAllStocks()
        {
            return _stockRepository.GetAllStocks();
        }

        public Task<List<Stock>?> GetCompanyStocks(int companyId)
        {
            return _stockRepository.GetCompanyStocks(companyId);
        }
    }
}