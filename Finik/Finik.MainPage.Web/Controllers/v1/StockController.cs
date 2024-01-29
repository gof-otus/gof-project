using Finik.MainPage.Core.Facade;
using Finik.MainPage.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finik.MainPage.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private IStockFacade _stockFacade;

        public StockController(IStockFacade stockFacade)
        {
            _stockFacade = stockFacade;
        }

        [HttpGet("{id}")]
        public Task<Stock?> GetStock(int id)
        {
            return _stockFacade.Get(id);
        }
    }
}