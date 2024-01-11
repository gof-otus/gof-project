using Finik.NewsService.Contracts;

namespace Finik.NewsService.Core.Abstractions.Services
{
    public interface INewsPublisher
    {
        void Publish(NewsDto newsDto);
    }
}
