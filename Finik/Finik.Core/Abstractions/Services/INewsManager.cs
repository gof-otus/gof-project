using Finik.Contracts;

namespace Finik.Core.Abstractions.Services;

public interface INewsManager
{
    public Task<IEnumerable<NewsDto>> GetAllNewsAsync();
    public Task<NewsDto?> GetNewsAsync(int id);
    public Task<NewsDto> CreateNewsAsync(NewsDto news);
    public Task DeleteNewsAsync(int id);
    public Task UpdateNewsAsync(NewsDto news);
}
