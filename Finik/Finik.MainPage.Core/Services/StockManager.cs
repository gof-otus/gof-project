using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;

namespace Finik.MainPage.Core.Services
{
    public class StockManager : IStockManager
    {
        private readonly IStockRepository _stockManager;

        public StockManager(IStockRepository stockManager)
        {
            _stockManager = stockManager;
        }

        public Task AddStock(Stock stock)
        {
            return _stockManager.AddStock(stock);
        }

        public Task<Stock?> GetStock(int id)
        {
            return _stockManager.GetStock(id);
        }
    }
}