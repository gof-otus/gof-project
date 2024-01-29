using Finik.MainPage.Core.Models;
using Finik.MainPage.Infrastructure.Interfaces;

namespace Finik.MainPage.Infrastructure.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly StockAndCompanyService _companyService;

        public CompanyService(StockAndCompanyService companyService)
        {
            _companyService = companyService;
        }

        public Task<Company?> GetCompany(int id)
        {
            return _companyService.Get<Company>("company", id);
        }
    }
}