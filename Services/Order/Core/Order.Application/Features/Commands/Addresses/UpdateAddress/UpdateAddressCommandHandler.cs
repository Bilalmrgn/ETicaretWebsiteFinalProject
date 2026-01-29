using MediatR;
using Order.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Features.Commands.Addresses.UpdateAddress
{
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommandRequest, UpdateAddressCommandResponse>
    {
        private readonly IAdressRepository _adressRepository;
        public UpdateAddressCommandHandler(IAdressRepository adressRepository)
        {
            _adressRepository = adressRepository;
        }

        public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommandRequest request, CancellationToken cancellationToken)
        {
            var address = await _adressRepository.GetByIdAsync(request.AddressId);

            if (address == null)
            {
                throw new Exception("Güncellenmek için adres bulunamadı.(UpdateAddressCommandHandler sayfası)");
            }
            
            //güncelle
            address.AddressId = request.AddressId;
            address.UserId = request.UserId;
            address.AddressDetail = request.AddressDetail;
            address.City = request.City;
            address.District = request.District;

            //IAdressRepository ye gönder çünkü veritabanına kaydedilecek
            await _adressRepository.UpdateAsync(address);
            

            return new UpdateAddressCommandResponse
            {
                Message = "Adres başarıyla güncellendi."
            };

        }
    }
}
