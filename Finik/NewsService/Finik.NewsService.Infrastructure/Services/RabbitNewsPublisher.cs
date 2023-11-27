using Finik.NewsService.Contracts;
using Finik.NewsService.Core.Abstractions.Services;

namespace Finik.NewsService.Infrastructure.Services
{
    public class RabbitNewsPublisher : INewsPublisher
    {
        public async Task Publish(NewsDto newsDto)
        {
            throw new NotImplementedException();
        }
    }
}
