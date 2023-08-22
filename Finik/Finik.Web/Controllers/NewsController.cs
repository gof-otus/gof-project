using Finik.Contracts;
using Finik.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finik.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsManager _newsManager;
        public NewsController(INewsManager newsManager)
        {
            _newsManager = newsManager;
        }

        [HttpGet]
        public async Task<IReadOnlyList<NewsDto>> Get()
        {
            return await _newsManager.GetAllNewsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var news = await _newsManager.GetNewsAsync(id);
            if (news == null) { return NotFound(); }
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewsDto value)
        {
            var result = await _newsManager.CreateNewsAsync(value);
            if (result == null) { return BadRequest(); }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] NewsDto value)
        {
            value.Id = id;
            await _newsManager.UpdateNewsAsync(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
           await _newsManager.DeleteNewsAsync(id);
        }
    }
}
