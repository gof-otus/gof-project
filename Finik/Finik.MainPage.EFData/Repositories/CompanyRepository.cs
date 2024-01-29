using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;

namespace Finik.MainPage.EFData.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly MainPageDbContext _context;

        public CompanyRepository(MainPageDbContext mainPageDbContext)
        {
            _context = mainPageDbContext;
        }

        public async Task AddCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteCompany(int id)
        //{
        //    var company = await _context.Companies.FindAsync(id);
        //    if (company != null)
        //    {
        //        _context.Companies.Remove(company);
        //        await _context.SaveChangesAsync();
        //    }
        //}

        public async Task<Company?> GetCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            return company;
        }

        //public async Task UpdateCompany(Company company)
        //{
        //    var companyOld = await _context.Companies.FindAsync(company.Id);
        //    if (companyOld != null)
        //    {
        //        _context.Companies.Remove(companyOld);
        //    }
        //    await _context.Companies.AddAsync(company);
        //    await _context.SaveChangesAsync();
        //}
    }
}
