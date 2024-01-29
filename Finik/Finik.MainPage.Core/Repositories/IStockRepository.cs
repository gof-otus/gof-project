using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Repositories
{
    public interface IStockRepository
    {
        public Task<Stock?> GetStock(int id);
        public Task AddStock(Stock stock);
        //public Task UpdateStock(Stock stock);
        //public Task DeleteStock(int id);
    }
}