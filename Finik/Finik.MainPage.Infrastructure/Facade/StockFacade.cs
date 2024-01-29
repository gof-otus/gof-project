using Finik.MainPage.Core.Facade;
using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Finik.MainPage.Infrastructure.Interfaces;

namespace Finik.MainPage.Infrastructure.Facade
{
    public class StockFacade : IStockFacade
    {
        private IStockManager _stockManager;
        private IStockService _stockService;

        public StockFacade(IStockManager stockManager, IStockService stockService)
        {
            _stockManager = stockManager;
            _stockService = stockService;
        }

        public async Task<Stock?> Get(int id)
        {
            var stock = await _stockManager.GetStock(id);
            if (stock == null)
            {
                stock = await _stockService.GetStock(id);
                if (stock != null)
                    await _stockManager.AddStock(stock);
            }
            return stock; 
        }
    }
}