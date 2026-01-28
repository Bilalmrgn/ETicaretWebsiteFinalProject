using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Address.GetByIdAddress
{
    //ben id ye sahip bütün adres bilgilerini getirmek istiyorum bu yüzden bütün modellerimi yazmam gerekiyor
    public class GetByIdAddressQueryResponse
    {
        public int AddressId { get; set; }
        public string UserId { get; set; }
        public string District { get; set; }//ilçe
        public string City { get; set; }
        public string AddressDetail { get; set; }

    }
}
