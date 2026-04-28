using Basket.Dtos;

namespace Basket.Service.Interfaces
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasketAsync(string userId);
        Task SaveAsync(BasketTotalDto basket);
        Task DeleteAsync(string userId);
        Task<string> ApplyDiscountAsync(string userId, string discountCode);
    }
}
