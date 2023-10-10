using Finik.Models;

namespace Finik.DbData;

public interface INewsDbRepository
{
    public Task<IReadOnlyList<News>> GetAllNews();
    public Task<News?> GetNews(Guid id);
    public Task<News> CreateNews (News news);
    public Task DeleteNews(Guid id);
    public Task UpdateNews(News news);
}