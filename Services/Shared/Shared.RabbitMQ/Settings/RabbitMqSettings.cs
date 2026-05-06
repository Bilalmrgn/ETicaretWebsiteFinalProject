using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ.Settings
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string VirtualHost { get; set; } = default!;
        public int Port { get; set; }

        public string PaymentCompletedExchange { get; set; }
            = "payment.completed.exchange";
    }
}
