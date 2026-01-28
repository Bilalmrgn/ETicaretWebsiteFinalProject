using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Address.GetByIdAddress
{
    public class GetByIdAddressQueryRequest : IRequest<GetByIdAddressQueryResponse>
    {
        //controller da getbyId kısmında parametre verdiğim için burada da parametre modeli tanımladım
        public int AddressId { get; set; }
        public GetByIdAddressQueryRequest(int addressId)
        {
            AddressId = addressId;
        }
    }
}
