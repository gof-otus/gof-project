using Finik.Contracts;

namespace Finik.Core.Abstractions.Services;

public interface INewsManager
{
    public Task<IReadOnlyList<NewsDto>> GetAllNews();
    public Task<NewsDto?> GetNews(int id);
    public Task<NewsDto> CreateNews(NewsDto news);
    public Task DeleteNews(int id);
    public Task UpdateNews(NewsDto news);
}
