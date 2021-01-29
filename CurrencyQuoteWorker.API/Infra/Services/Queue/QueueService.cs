using CurrencyQuoteWorker.API.Excepions;
using CurrencyQuoteWorker.API.Infra.Configuration;
using CurrencyQuoteWorker.API.Infra.Interfaces.Services.Queue;
using CurrencyQuoteWorkerService.Shared.DTOs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace CurrencyQuoteWorker.API.Infra.Services.Queue
{
    public class QueueService : IQueueService
    {
        private readonly GeneralConfiguration _configuration;
        private readonly IConnection _connection;
        public QueueService(IOptions<GeneralConfiguration> settings)
        {
            _configuration = settings.Value;
            _connection = new ConnectionFactory().CreateConnection(_configuration.QueueHostConnection);
        }

        public void Publish(CurrencyByDateDTO[] records)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _configuration.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(records);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _configuration.QueueName,
                                     basicProperties: null,
                                     body: body);
            }
        }

        public CurrencyByDateDTO[] GetNextMessage()
        {
            using (var channel = _connection.CreateModel())
            {
                var queueDeclareResponse = channel.QueueDeclare(_configuration.QueueName, false, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume(_configuration.QueueName, true, consumer);

                if (queueDeclareResponse.MessageCount == 0)
                    throw new NotFoundException("");

                var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                var result = JsonSerializer.Deserialize<CurrencyByDateDTO[]>(message);

                return result;
            }
        }
    }
}
