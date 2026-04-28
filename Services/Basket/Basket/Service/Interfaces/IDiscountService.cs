using Basket.Dtos;

namespace Basket.Service.Interfaces
{
    public interface IDiscountService
    {
        Task<ResultCouponDto?> GetByCodeAsync(string code);
    }
}
