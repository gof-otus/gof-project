using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finik.StocksAndCompanies.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyManager _companyManager;
        public CompanyController(ICompanyManager companyManager)
        {
            _companyManager = companyManager;
        }

        // GET: api/<CompanyController>
        [HttpGet]
        public async Task<List<Company>> Get()
        {
            return await _companyManager.GetAllCompanies();
        }

        // GET api/<CompanyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> Get(int id)
        {
            var company = await _companyManager.GetCompany(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }

        // GET api/<CompanyController>/ForStock/5
        [HttpGet("ForStock/{id}")]
        public async Task<ActionResult<Company>> GetByStockId(int id)
        {
            var company = await _companyManager.GetCompanyForStock(id);
            if (company == null)
                return NotFound();
            return Ok(company);
        }
    }
}
