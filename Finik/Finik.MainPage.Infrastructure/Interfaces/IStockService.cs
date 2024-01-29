using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Infrastructure.Interfaces
{
    public interface IStockService
    {
        public Task<Stock?> GetStock(int id);
    }
}