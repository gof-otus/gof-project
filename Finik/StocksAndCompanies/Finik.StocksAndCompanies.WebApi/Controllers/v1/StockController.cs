using Finik.StockAndCompany.Core.Interfaces;
using Finik.StockAndCompany.Core.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Finik.StocksAndCompanies.WebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockManager _stockManager;
        public StockController(IStockManager stockManager)
        {
            _stockManager = stockManager;
        }

        //// GET: api/<StockController>
        //[HttpGet]
        //public async Task<List<Stock>> Get()
        //{
        //    return await _stockManager.GetAllStocks();
        //}

        // GET api/<StockController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> Get(int id)
        {
            var stock = await _stockManager.GetStock(id);
            if (stock == null)
                return NotFound();
            return Ok(stock);
        }

        // GET api/<StockController>/5
        [HttpGet("ForCompany/{id}")]
        public async Task<ActionResult<Stock>> GetByCompanyId(int id)
        {
            var stocks = await _stockManager.GetCompanyStocks(id);
            if (stocks == null)
                return NotFound();
            return Ok(stocks);
        }
    }
}