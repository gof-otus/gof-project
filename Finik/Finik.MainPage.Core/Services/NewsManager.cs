using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Core.Models;
using Finik.MainPage.Core.Repositories;

namespace Finik.MainPage.Core.Services
{
    public class NewsManager : INewsManager
    {
        private readonly INewsRepository _newsManager;

        public NewsManager(INewsRepository newsManager)
        {
            _newsManager = newsManager;
        }

        public Task<News?> GetNews(int id) 
        {
            return _newsManager.GetNews(id);
        }

        public Task AddNews(News news)
        {
            return _newsManager.AddNews(news);
        }

        public Task UpdateNews(News news)
        {
            return _newsManager.UpdateNews(news);
        }

        public Task DeleteNews(int id) 
        {  
            return _newsManager.DeleteNews(id);
        }

        public Task<IEnumerable<News>> GetLastNews()
        {
            return _newsManager.GetLastNews();
        }
    }
}