using Finik.StockAndCompany.Core.Models;

namespace Finik.StockAndCompany.Core.Repositories
{
    public interface ICompanyRepository
    {
        public Task<int> CreateCompany(Company company);
        public Task<Company?> GetCompany(int id);
        public Task DeleteCompany(int id);
        public Task<List<Company>> GetAllCompanies();
        public Task<Company?> GetCompanyForStock(int stockId);
    }
}