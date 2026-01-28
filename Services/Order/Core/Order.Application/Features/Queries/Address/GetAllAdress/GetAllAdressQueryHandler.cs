using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Queries.Address.GetAllAdress
{
    public class GetAllAdressQueryHandler : IRequestHandler<GetAllAdressQueryRequest, List<GetAllAdressQueryResponse>>
    {
        private readonly IAdressRepository _adressRepository;

        public GetAllAdressQueryHandler(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public async Task<List<GetAllAdressQueryResponse>> Handle(GetAllAdressQueryRequest request, CancellationToken cancellationToken)
        {
            var address = await _adressRepository.GetAllAsync();

            var response = address.Select(p => new GetAllAdressQueryResponse
            {
                AddressId = p.AddressId,
                UserId = p.UserId,
                City = p.City,
                District = p.District,
                AddressDetail = p.AddressDetail
            }).ToList();

            return response;
        }
    }
}
