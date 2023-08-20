using Finik.Models;

namespace Finik.DbData;

public interface INewsDbRepository
{
    public Task<IEnumerable<News>> GetAllNewsAsync();
    public Task<News?> GetNewsAsync(int id);
    public Task<News> CreateNewsAsync (News news);
    public Task DeleteNewsAsync(int id);
    public Task UpdateNewsAsync(News news);
}