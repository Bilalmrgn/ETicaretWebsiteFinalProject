using ECommerce.WebAPI.Dtos;

namespace ECommerce.WebAPI.Services
{
    public interface IContactService
    {
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task<List<ResultContactDto>> GetAllContactsAsync();
        Task DeleteContactAsync(int id);
       /* Task<UpdateContactDto> UpdateContactAsync(UpdateContactDto updateContactDto);//mesaj okundu mu okunmadı mı bilgisini değiştirmek için*/
        Task<GetByIdContactDto> GetByIdContactAsync(int id);
    }
}
