using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finik.MainPage.Web.Controllers.v1
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
        public Task<IEnumerable<News>> GetLastNews()
        {
            return _newsManager.GetLastNews();
        }

        [HttpGet("{id}")]
        public Task<News?> GetNews(int id)
        {
            return _newsManager.GetNews(id);
        }
    }
}
