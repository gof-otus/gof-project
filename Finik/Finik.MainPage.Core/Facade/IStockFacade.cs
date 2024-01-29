using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Facade
{
    public interface IStockFacade
    {
        public Task<Stock?> Get(int id);
    }
}