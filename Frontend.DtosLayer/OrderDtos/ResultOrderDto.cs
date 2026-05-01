using Frontend.DtosLayer.OrderDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.OrderDtos
{
    public class ResultOrderDto
    {
        public int OrderingId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }

        public string City { get; set; }
        public string District { get; set; }
        public string AddressDetail { get; set; }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<ResultOrderDetailDto> OrderDetails { get; set; }
    }
}
