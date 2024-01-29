using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Infrastructure.Interfaces
{
    public interface ICompanyService
    {
        public Task<Company?> GetCompany(int id);
    }
}