using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.WebAPI.Dtos.CreditCardDtos;
using Payment.WebAPI.Services.Interface;

namespace Payment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // zorunlu
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        // Kart ekleme
        [HttpPost]
        public async Task<IActionResult> CreateCreditCard(CreateCreditCardDto dto)
        {
            await _creditCardService.CreateCreditCardAsync(dto);
            return Ok("Kart başarıyla eklendi");
        }

        // Kullanıcının tüm kartları
        [HttpGet]
        public async Task<IActionResult> GetAllCreditCards()
        {
            var result = await _creditCardService.GetAllCreditCardAsync();
            return Ok(result);
        }

        [HttpGet("ByUserId")]
        public async Task<IActionResult> GetCreditCardByUserId()
        {
            var result = await _creditCardService.GetCreditCardByUserId();
            return Ok(result);
        }

        //Kart güncelleme
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCreditCard(int id,UpdateCreditCardDto dto)
        {
            dto.CreditCardId = id;
            await _creditCardService.UpdateCreditCardAsync(dto);
            return Ok("Kart güncellendi");
        }

        [HttpGet("ByUserId/{userId}")]
        public async Task<IActionResult> GetByUserName(string userId)
        {
            var result = await _creditCardService.GetCreditCardsByUserNameAsync(userId);
            return Ok(result);
        }
    }
}