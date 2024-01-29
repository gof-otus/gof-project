using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Interfaces
{
    public interface IStockManager
    {
        public Task<Stock?> GetStock(int id);
        public Task AddStock(Stock stock);
        //public Task UpdateStock(Stock stock);
        //public Task DeleteStock(int id);
    }
}