using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/newss")]
    public class NewsController : Controller
    {
        static readonly List<News> data;
        static NewsController()
        {
            data = new List<News>();
        }
        [HttpGet]
        public IEnumerable<News> Get()
        {
            return data;
        }

        [HttpPost]
        public IActionResult Post(News news)
        {
            news.Id = Guid.NewGuid().ToString();
            data.Add(news);
            return Ok(news);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            News news = data.FirstOrDefault(x => x.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            data.Remove(news);
            return Ok(news);
        }
    }
}
