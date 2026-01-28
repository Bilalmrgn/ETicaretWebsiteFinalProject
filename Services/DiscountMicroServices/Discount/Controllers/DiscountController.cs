using Discount.Dtos;
using Discount.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coupons = await _discountService.GetAllCouponAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CouponListById(int id)
        {
            var value = _discountService.GetByIdCouponAsync(id);

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CreateCouponDto createCoupon)
        {
            await _discountService.CreateCouponAsync(createCoupon);

            return Ok("Kupon başarıyla oluşturuldu");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);

            return Ok("Kupon başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoupon(UpdateCouponDto updateCoupon)
        {
            await _discountService.UpdateCouponAsync(updateCoupon);

            return Ok("Kupon başarıyla güncellendi.");
        }
    }
}
