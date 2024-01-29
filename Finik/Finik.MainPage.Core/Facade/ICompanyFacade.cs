using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Facade
{
    public interface ICompanyFacade
    {
        public Task<Company?> Get(int id);
    }
}
