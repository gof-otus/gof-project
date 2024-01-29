using Finik.MainPage.Core.Models;
using Finik.MainPage.Infrastructure.Interfaces;

namespace Finik.MainPage.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly StockAndCompanyService _stockService;
        public StockService(StockAndCompanyService service) 
        { 
            _stockService = service;
        }

        public Task<Stock?> GetStock(int id)
        {
            return _stockService.Get<Stock>("stock", id); 
        }
    }
}