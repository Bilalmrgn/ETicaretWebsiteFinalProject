using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RabbitMQ
{
    public class PaymentCompletedEvent
    {
        //bu sınıfın amacı payment completed olduktan sonra hangi bilgileri taşıyacağız

        public int OrderId { get; set; }

        public string UserId { get; set; } = default!;

        public string Email { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
