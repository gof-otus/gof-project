using Finik.MainPage.Core.Facade;
using Finik.MainPage.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finik.MainPage.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyFacade _companyFacade;

        public CompanyController(ICompanyFacade companyFacade)
        {
            _companyFacade = companyFacade;
        }

        [HttpGet("{id}")]
        public Task<Company?> GetCompany(int id)
        {
            return _companyFacade.Get(id);
        }
    }
}