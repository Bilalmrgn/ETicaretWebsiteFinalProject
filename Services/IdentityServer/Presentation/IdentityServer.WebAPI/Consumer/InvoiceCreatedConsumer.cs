using IdentityServer.Infrastructure.EmailService;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Settings;
using System.Text;
using System.Text.Json;

namespace IdentityServer.WebAPI.Consumer
{
    public class InvoiceCreatedConsumer : BackgroundService
    {
        private readonly RabbitMqSettings _settings;

        private readonly IServiceProvider _serviceProvider;

        private IConnection? _connection;

        private IModel? _channel;

        private const string QueueName =
            "mail.invoice.created.queue";

        public InvoiceCreatedConsumer(
            IServiceProvider serviceProvider,
            IOptions<RabbitMqSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;
        }

        public override Task StartAsync(
            CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                VirtualHost = _settings.VirtualHost,
                Port = _settings.Port
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            // INVOICE CREATED EXCHANGE
            // DEĞİŞTİRDİĞİN KISIM
            _channel.ExchangeDeclare(
                exchange: _settings.InvoiceCreatedExchange,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false);

            // QUEUE
            _channel.QueueDeclare(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false);

            // BIND
            _channel.QueueBind(
                queue: QueueName,
                exchange: _settings.InvoiceCreatedExchange,
                routingKey: "");

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var consumer =
                new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();

                var json =
                    Encoding.UTF8.GetString(body);

                var invoiceEvent =
                    JsonSerializer.Deserialize<InvoiceCreatedEvent>(json);

                using var scope =
                    _serviceProvider.CreateScope();

                var emailService =
                    scope.ServiceProvider
                    .GetRequiredService<IEmailService>();

                await emailService.SendEmailPaymentSuccess(
                    invoiceEvent.Email,
                    invoiceEvent.PdfBytes,
                    invoiceEvent.FileName);

                _channel.BasicAck(
                    e.DeliveryTag,
                    false);
            };

            _channel.BasicConsume(
                queue: QueueName,
                autoAck: false,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }
}