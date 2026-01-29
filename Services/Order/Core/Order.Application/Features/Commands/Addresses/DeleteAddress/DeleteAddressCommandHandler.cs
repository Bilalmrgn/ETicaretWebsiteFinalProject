using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Addresses.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommandRequest, DeleteAddressCommandResponse>
    {
        private readonly IAdressRepository _addressRepository;

        public DeleteAddressCommandHandler(IAdressRepository adressRepository)
        {
            _addressRepository = adressRepository;
        }

        public async Task<DeleteAddressCommandResponse> Handle(DeleteAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.AddressId);

            if (address == null)
            {
                throw new Exception("Adres bulunamadı.(DeleteAddressCommandHandler sayfasında.)");
            }

            await _addressRepository.DeleteAsync(address);

            return new DeleteAddressCommandResponse
            {
                Message = "Adres başarıyla silindi."
            };
        }
    }
}
