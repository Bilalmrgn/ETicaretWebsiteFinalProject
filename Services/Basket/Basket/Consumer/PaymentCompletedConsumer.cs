
using Basket.Service.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Settings;
using System.Text;

namespace Basket.Consumer
{
    public class PaymentCompletedConsumer : BackgroundService
    {
        private readonly RabbitMqSettings _settings;
        private readonly IServiceProvider _serviceProvider;
        private IConnection? _connection;
        private IModel? _channel;
        private const string QueueName = "basket.payment.completed.queue";

        public PaymentCompletedConsumer(IServiceProvider serviceProvider, IOptions<RabbitMqSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
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

            _channel.ExchangeDeclare(
                exchange: _settings.PaymentCompletedExchange,
                type: ExchangeType.Fanout,
                durable: true,
                autoDelete: false
            );

            _channel.QueueDeclare(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
                );

            _channel.QueueBind(
           queue: QueueName,
           exchange: _settings.PaymentCompletedExchange,
           routingKey: ""
       );

            return base.StartAsync(cancellationToken);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();

                var json = Encoding.UTF8.GetString(body);

                var paymentEvent = System.Text.Json.JsonSerializer.Deserialize<PaymentCompletedEvent>(json);

                //redisten sepet silinir. Çünkü ben ödeme başarılı mesajını rabbitmq dan aldım
                using var scope = _serviceProvider.CreateScope();

                var basketService = scope.ServiceProvider.GetRequiredService<IBasketService>();

                await basketService.DeleteAsync(paymentEvent.UserId);

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
