using Finik.NewsService.Contracts;

namespace Finik.NewsService.Core.Abstractions.Services
{
    public interface INewsPublisher
    {
        Task Publish(NewsDto newsDto);
    }
}
