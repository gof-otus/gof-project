using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Core.Repositories
{
    public interface INewsRepository
    {
        public Task<IEnumerable<News>> GetLastNews();
        public Task<News?> GetNews(int id);
        public Task AddNews(News item);
        public Task UpdateNews(News item);
        public Task DeleteNews(int id);
    }
}