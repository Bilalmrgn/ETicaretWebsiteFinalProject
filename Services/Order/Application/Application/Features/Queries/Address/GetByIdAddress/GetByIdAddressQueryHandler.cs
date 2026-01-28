using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Address.GetByIdAddress
{
    public class GetByIdAddressQueryHandler : IRequestHandler<GetByIdAddressQueryRequest, GetByIdAddressQueryResponse>
    {
        readonly IAdressRepository _adressRepository;
        public GetByIdAddressQueryHandler(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public async Task<GetByIdAddressQueryResponse> Handle(GetByIdAddressQueryRequest request, CancellationToken cancellationToken)
        {
            var address = await _adressRepository.GetByIdAsync(request.AddressId);

            if (address == null)
            {
                throw new Exception("Adres bulunamadı");
            }

            //aşağıdaki gibi yaptık çünkü GetById tek nesne döner GetAll ise koleksyon döner bu yüzden GetAll kısmında Select yaparız
            return new GetByIdAddressQueryResponse
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                District = address.District,
                City = address.City,
                AddressDetail = address.AddressDetail
            };
        }
    }
}
