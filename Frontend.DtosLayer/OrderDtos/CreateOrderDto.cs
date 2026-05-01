using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.DtosLayer.OrderDtos
{
    public class CreateOrderDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        //adres ile ilgili bilgiler
        public string City { get; set; }
        public string District { get; set; }
        public string AddressDetail { get; set; }
    }
}
