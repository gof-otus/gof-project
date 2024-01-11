using Finik.NewsService.Contracts;
using Finik.NewsService.Core.Abstractions.Services;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Finik.NewsService.Infrastructure.Services
{
    public class RabbitNewsPublisher : INewsPublisher
    {
        private const string PublishedNewsQueueName = "PublishedNews";
        private readonly RabbitMqOptions _rabbitMqOptions;

        public RabbitNewsPublisher(IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _rabbitMqOptions = rabbitMqOptions.Value;
        }
        public void Publish(NewsDto newsDto)
        {
            var message = JsonSerializer.Serialize(newsDto);

            var factory = new ConnectionFactory() { HostName = _rabbitMqOptions.Host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(PublishedNewsQueueName, exclusive: false);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: string.Empty,
                    routingKey: PublishedNewsQueueName,
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
