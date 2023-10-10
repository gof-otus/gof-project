using Finik.Contracts;
using Finik.Core.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Finik.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
            return await _newsManager.GetAllNews();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var news = await _newsManager.GetNews(id);
            if (news == null) { return NotFound(); }
            return Ok(news);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewsDto value)
        {
            var result = await _newsManager.CreateNews(value);
            if (result == null) { return BadRequest(); }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] NewsDto value)
        {
            value.Id = id;
            await _newsManager.UpdateNews(value);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _newsManager.DeleteNews(id);
        }
    }
}
