using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Addresses.UpdateAddress
{
    public class UpdateAddressCommandRequest : IRequest<UpdateAddressCommandResponse>
    {
        public int AddressId { get; set; }
        public string UserId { get; set; }
        public string District { get; set; }//ilçe
        public string City { get; set; }
        public string AddressDetail { get; set; }
    }
}
