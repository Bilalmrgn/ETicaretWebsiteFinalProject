using Basket.Dtos;

namespace Basket.Service.Interfaces
{
    public interface IDiscountService
    {
        Task<ResultCouponDto?> GetByCodeAsync(string code);
        Task<string> ApplyDiscountAsync(string userId, string code);
    }
}
