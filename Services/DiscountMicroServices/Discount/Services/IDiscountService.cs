using Discount.Dtos;

namespace Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultCouponDto>> GetAllCouponAsync();
        Task CreateCouponAsync(CreateCouponDto createCouponDto);
        Task UpdateCouponAsync(UpdateCouponDto updateCouponDto);
        Task DeleteCouponAsync(int couponId);
        Task<GetByIdCoupon> GetByIdCouponAsync(int couponId);
    }
}
