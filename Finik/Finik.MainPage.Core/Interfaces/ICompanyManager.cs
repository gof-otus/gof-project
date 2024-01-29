using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Interfaces
{
    public interface ICompanyManager
    {
        public Task<Company?> GetCompany(int id);
        public Task AddCompany(Company company);
        //public Task UpdateCompany(Company company);
        //public Task DeleteCompany(int id);
    }
}