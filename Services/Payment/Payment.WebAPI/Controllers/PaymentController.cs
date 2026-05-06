using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.WebAPI.Dtos.PaymentDtos;
using Payment.WebAPI.Services.Interface;

namespace Payment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Pay([FromBody] PaymentRequestDto dto)
        {
            var result = await _paymentService.ProcessPaymentAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
