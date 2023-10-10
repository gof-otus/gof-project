using Finik.NewsService.Contracts;

namespace Finik.NewsService.Core.Abstractions.Services;

public interface INewsManager
{
    public Task<IReadOnlyList<NewsDto>> GetAllNews();
    public Task<NewsDto?> GetNews(Guid id);
    public Task<NewsDto> CreateNews(NewsDto news);
    public Task DeleteNews(Guid id);
    public Task UpdateNews(NewsDto news);
}
