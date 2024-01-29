using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Repositories
{
    public interface ICompanyRepository
    {
        public Task<Company?> GetCompany(int id);
        public Task AddCompany(Company company);
        //public Task UpdateCompany(Company company);
        //public Task DeleteCompany(int id);
    }
}