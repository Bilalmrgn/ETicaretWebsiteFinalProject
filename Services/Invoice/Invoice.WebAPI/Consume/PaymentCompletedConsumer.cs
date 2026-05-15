using Invoice.WebAPI.Model;
using Invoice.WebAPI.Publisher;
using Invoice.WebAPI.Services.Interface;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Settings;
using System.Text;

namespace Invoice.WebAPI.Consume
{
    public class PaymentCompletedConsumer : BackgroundService
    {
        private readonly RabbitMqSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        private IConnection? _connection;
        private IModel? _channel;

        private const string QueueName =
            "invoice.payment.completed.queue";

        public PaymentCompletedConsumer(
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

            // PAYMENT COMPLETED EXCHANGE
            _channel.ExchangeDeclare(
                exchange: _settings.PaymentCompletedExchange,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false
            );

            // QUEUE
            _channel.QueueDeclare(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            // BIND
            _channel.QueueBind(
                queue: QueueName,
                exchange: _settings.PaymentCompletedExchange,
                routingKey: ""
            );

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();

                var json = Encoding.UTF8.GetString(body);

                var paymentEvent =
                    System.Text.Json.JsonSerializer
                    .Deserialize<PaymentCompletedEvent>(json);

                using var scope =
                    _serviceProvider.CreateScope();

                // INVOICE SERVICE
                var invoiceService =
                    scope.ServiceProvider
                    .GetRequiredService<IInvoiceService>();

                // PDF SERVICE
                var pdfService =
                    scope.ServiceProvider
                    .GetRequiredService<IPdfService>();

                // PUBLISHER SERVICE
                // EKLEDİĞİN YENİ KISIM
                var publisher =
                    scope.ServiceProvider
                    .GetRequiredService<IInvoiceCreatedPublisher>();

                // INVOICE MODEL
                var invoice = new InvoiceModel
                {
                    OrderId = paymentEvent.OrderId,
                    UserId = paymentEvent.UserId,
                    Email = paymentEvent.Email,
                    TotalPrice = paymentEvent.TotalPrice,
                    InvoiceNumber = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    PdfPath = "invoice.pdf",
                    CreatedDate = DateTime.UtcNow
                };

                // DATABASE SAVE
                await invoiceService.CreateAsync(invoice);

                // PDF GENERATE
                var pdfBytes =
                    pdfService.GeneratePdf(invoice);

                // EVENT OLUŞTUR
                // EKLEDİĞİN YENİ KISIM
                var invoiceCreatedEvent =
                    new InvoiceCreatedEvent
                    {
                        Email = paymentEvent.Email,
                        PdfBytes = pdfBytes,
                        FileName = "invoice.pdf"
                    };

                // RABBITMQ PUBLISH
                publisher.Publish(invoiceCreatedEvent);

                // ACK
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