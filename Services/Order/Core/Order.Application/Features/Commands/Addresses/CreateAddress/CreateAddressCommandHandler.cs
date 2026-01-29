using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Order.Domain;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Addresses.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommandRequest, CreateAddressCommandResponse>
    {
        private readonly IAdressRepository _adressRepository;
        public CreateAddressCommandHandler(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }
        public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            await _adressRepository.CreateAsync(new Address()
            {
                UserId = request.UserId,
                District = request.District,
                AddressDetail = request.AddressDetail,
                City = request.City
            });


            return new CreateAddressCommandResponse
            {
                Message = "adres başarıyla oluşturuldu."
            };
            

        }
    }
}