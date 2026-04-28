using Discount.API.Dtos;
using Discount.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> DiscountCouponList()
        {
            var values = await _discountService.GetAllCouponAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            var values = await _discountService.GetByIdCouponAsync(id);
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("getbycode/{code}")]
        public async Task<IActionResult> GetDiscountCouponByCode(string code)
        {
            var value = await _discountService.GetByCodeAsync(code);

            if (value == null)
                return NotFound("Kupon bulunamadı");

            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateCouponDto dto)
        {
            await _discountService.CreateCouponAsync(dto);
            return Ok("Kupon eklendi");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            await _discountService.DeleteCouponAsync(id);
            return Ok("Silindi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiscountCoupon(int id, UpdateCouponDto dto)
        {
            dto.CouponId = id; // önemli!
            await _discountService.UpdateCouponAsync(dto);
            return Ok("Güncellendi");
        }
    }
}
