using Finik.NewsService.Contracts;

namespace Finik.NewsService.Core.Abstractions.Services;

public interface INewsManager
{
    Task<IReadOnlyList<NewsDto>> GetAllNews();
    Task<NewsDto?> GetNews(Guid id);
    Task<NewsDto> CreateNews(NewsDto news);
    Task DeleteNews(Guid id);
    Task UpdateNews(NewsDto news);
    Task Publish(NewsDto news);
}
