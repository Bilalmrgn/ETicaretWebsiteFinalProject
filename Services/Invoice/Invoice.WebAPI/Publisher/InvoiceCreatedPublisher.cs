using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Settings;
using System.Text;
using System.Text.Json;

namespace Invoice.WebAPI.Publisher
{
    public class InvoiceCreatedPublisher : IInvoiceCreatedPublisher
    {
        private readonly RabbitMqSettings _settings;

        public InvoiceCreatedPublisher(
            IOptions<RabbitMqSettings> settings)
        {
            _settings = settings.Value;
        }

        public void Publish(
            InvoiceCreatedEvent invoiceCreatedEvent)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost,
                Port = _settings.Port
            };

            using var connection =
                factory.CreateConnection();

            using var channel =
                connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: _settings.InvoiceCreatedExchange,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false);

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(invoiceCreatedEvent));

            channel.BasicPublish(
                exchange: _settings.InvoiceCreatedExchange,
                routingKey: "",
                basicProperties: null,
                body: body);
        }
    }
}
