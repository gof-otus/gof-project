using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Finik.MainPage.Infrastructure;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Finik.MainPage.Core.Interfaces;
using Finik.MainPage.Contracts;
using Finik.MainPage.Core.Models;

namespace Finik.MainPage.Web.Rabbit
{
    public class Listener : BackgroundService
    {
        private IMapper _mapper;
        private IConnection _connection;
        private IModel _channel;
        private INewsManager _newsManager;
        private const string PublishedNewsQueueName = "PublishedNews";
        private readonly RabbitMqOptions _rabbitMqOptions;

        public Listener(IMapper mapper, IOptions<RabbitMqOptions> rabbitMqOptions, INewsManager newsManager)
        {
            _mapper = mapper;
            _rabbitMqOptions = rabbitMqOptions.Value;
            _newsManager = newsManager;
            var factory = new ConnectionFactory 
            { 
                HostName = _rabbitMqOptions.Host, 
                Port = 5677, 
                UserName = "finik", 
                Password = "otus5"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(PublishedNewsQueueName, exclusive: false);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received +=
                (sender, eventArgs) =>
                {
                    var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    var newsDto = JsonSerializer.Deserialize<NewsDto>(body);
                    var newsEntity = _mapper.Map<News>(newsDto);
                    _newsManager.AddNews(newsEntity);
                    //todo - принимать не просто новости, но команды на создание\добавление\удаление
                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                };
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}