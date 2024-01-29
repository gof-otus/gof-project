using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;

namespace Finik.MainPage.Core.Services
{
    public class CompanyManager : ICompanyManager
    {
        private readonly ICompanyRepository _companyManager;
        public CompanyManager(ICompanyRepository companyManager) 
        {
            _companyManager = companyManager;
        }

        public Task<Company?> GetCompany(int id)
        {
            return _companyManager.GetCompany(id);
        }

        public Task AddCompany(Company company)
        {
            return _companyManager.AddCompany(company);
        }

        //public Task UpdateCompany(Company company)
        //{
        //    return _companyManager.UpdateCompany(company);
        //}

        //public Task DeleteCompany(int id) 
        //{  
        //    return _companyManager.DeleteCompany(id);
    //}
    }
}