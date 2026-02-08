using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Addresses.CreateAddress
{
    public class CreateAddressCommandRequest : IRequest<CreateAddressCommandResponse>
    {
        public string UserId { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string AddressDetail { get; set; }
    }

    
}
