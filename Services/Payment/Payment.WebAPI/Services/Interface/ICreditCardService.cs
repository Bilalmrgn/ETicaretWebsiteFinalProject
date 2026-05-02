using Payment.WebAPI.Dtos.CreditCardDtos;

namespace Payment.WebAPI.Services.Interface
{
    public interface ICreditCardService
    {
        Task CreateCreditCardAsync(CreateCreditCardDto dto);
        Task UpdateCreditCardAsync(UpdateCreditCardDto dto);
        Task<List<ResultCreditCardDto>> GetAllCreditCardAsync();
        Task<List<ResultCreditCardDto>> GetCreditCardByUserId();
        Task<List<ResultCreditCardDto>> GetCreditCardsByUserNameAsync(string userId);



    }
}
