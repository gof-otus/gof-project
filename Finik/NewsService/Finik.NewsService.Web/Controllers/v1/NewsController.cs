using Finik.NewsService.Contracts;
using Finik.NewsService.Core.Abstractions.Services;
using Finik.NewsService.Web.DTO;
using Finik.NewsService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finik.NewsService.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
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
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateNewsRequest updateNewsRequest)
        {          
            var news = await _newsManager.GetNews(id);

            if (news != null) 
            {
                var userId = HttpContext.User.FindFirst("id")?.Value;

                if (news.Author.ToString() != userId && HttpContext.User.IsInRole(Role.Author.ToString()))
                {
                    return Forbid();
                }

                await _newsManager.UpdateNews(news.FromUpdateRequest(updateNewsRequest));
                return Ok();
            }

            return NotFound();         
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!HttpContext.User.IsInRole(Role.Administrator.ToString()))
            {
                return Forbid();
            }

            await _newsManager.DeleteNews(id);
            return Ok();
        }

        [HttpPost("publish/{id}")]
        public async Task<ActionResult> Publish(Guid id)
        {
            var news = await _newsManager.GetNews(id);

            if (news != null)
            {
                if (HttpContext.User.IsInRole(Role.Author.ToString()))
                {
                    return Forbid();
                }

                await _newsManager.Publish(news);
                return Ok();
            }

            return NotFound();
        }
    }
}
