using Finik.Models;

namespace Finik.DbData;

public interface INewsDbRepository
{
    public Task<IReadOnlyList<News>> GetAllNews();
    public Task<News?> GetNews(int id);
    public Task<News> CreateNews (News news);
    public Task DeleteNews(int id);
    public Task UpdateNews(News news);
}