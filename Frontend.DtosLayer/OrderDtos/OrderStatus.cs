using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.OrderDtos
{
    public enum OrderStatus
    {
        //siparişin hangi aşamada olduğunu gösterir.
        Pending,
        Processing,
        Completed,
        Failed,
        Cancelled,
        Shipped
    }
}
