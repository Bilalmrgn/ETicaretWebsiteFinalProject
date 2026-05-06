using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.RabbitMQ;
using Shared.RabbitMQ.Settings;
using System.Text;
using System.Text.Json;

namespace Payment.WebAPI.Services
{
    public class RabbitMqPublisher
    {
        private readonly RabbitMqSettings _settings;

        public RabbitMqPublisher(IOptions<RabbitMqSettings> settings)
        {
            _settings = settings.Value;
        }

        public void PublishPaymentCompleted(PaymentCompletedEvent paymentEvent)
        {
            //rabbitmq bağlantısı için gerekli ayarlar
            var factory = new ConnectionFactory
            {
                HostName = _settings.HostName,
                UserName = _settings.UserName,
                Password = _settings.Password,
                Port = _settings.Port,
                VirtualHost = _settings.VirtualHost
            };

            //rabbitmq bağlantısı oluştur
            using var connection = factory.CreateConnection();

            //raqbbit mq üzerinde işlem yapabilmek için kanal oluşturulur
            using var channel = connection.CreateModel();
            
            //mesajların gönderileceği exchange oluşturulur
            channel.ExchangeDeclare(exchange: _settings.PaymentCompletedExchange,
            type: ExchangeType.Fanout,
            durable: true,
            autoDelete: false);

            var json = JsonSerializer.Serialize(paymentEvent);

            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(
           exchange: _settings.PaymentCompletedExchange,
           routingKey: "",
           body: body);
        }
    }
}
