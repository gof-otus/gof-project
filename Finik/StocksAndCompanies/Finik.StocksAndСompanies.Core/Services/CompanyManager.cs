using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Models;
using Finik.StockAndCompany.Core.Repositories;

namespace Finik.StockAndCompany.Core.Services
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyManager(ICompanyRepository companyRepository) 
        {
            _companyRepository = companyRepository;
        }

        public Task<Company?> GetCompany(int id)
        {
            return _companyRepository.GetCompany(id);
        }

        public Task<List<Company>> GetAllCompanies()
        { 
            return _companyRepository.GetAllCompanies();
        }

        public Task<Company?> GetCompanyForStock(int stockId)
        {
            return _companyRepository.GetCompanyForStock(stockId);
        }
    }
}