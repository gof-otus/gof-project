using Finik.StockAndCompany.Core.Models;

namespace Finik.StockAndCompany.Core.Interfaces
{
    public interface ICompanyManager
    {
        public Task<int> CreateCompany(Company company);
        public Task<Company?> GetCompany(int id);
        public Task<List<Company>> GetAllCompanies();
        public Task<Company?> GetCompanyForStock(int stockId);
    }
}