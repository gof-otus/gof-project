using Finik.MainPage.Core.Facade;
using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Finik.MainPage.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finik.MainPage.Infrastructure.Facade
{
    public class CompanyFacade : ICompanyFacade
    {
        private ICompanyManager _companyManager;
        private ICompanyService _companyService;

        public CompanyFacade(ICompanyManager companyManager, ICompanyService companyService)
        {
            _companyManager = companyManager;
            _companyService = companyService;
        }

        public async Task<Company?> Get(int id)
        {
            var company = await _companyManager.GetCompany(id);
            if  (company == null)
            {
                company = await _companyService.GetCompany(id);
                if (company != null)
                    await _companyManager.AddCompany(company);
            }
            return company;
        }
    }
}
