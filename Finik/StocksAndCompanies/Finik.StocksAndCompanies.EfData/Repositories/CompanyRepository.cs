using Finik.StockAndCompany.Core.Models;
using Finik.StockAndCompany.Core.Repositories;

namespace Finik.StocksAndCompanies.EfData.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly StockAndCompaniesDbContext _context;

        public CompanyRepository(StockAndCompaniesDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateCompany(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
            return company.Id;
        }
        public async Task<Company?> GetCompany(int id)
        { 
            var company = await _context.Companies.FindAsync(id);
            return company;
        }

        public async Task DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<Company>> GetAllCompanies()
        { 
            return Task.Run(() =>  _context.Companies.ToList());
        }

        public async Task<Company?> GetCompanyForStock(int stockId)
        { 
            var stock = await _context.Stocks.FindAsync(stockId);
            return stock?.Company;
        }
    }
}