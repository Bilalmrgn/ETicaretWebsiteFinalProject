using Catolog.DTOs.SpecialOfferDto;
using Catolog.Services.SpecialOfferService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOfferController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;
        public SpecialOfferController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        // kategori listesi
        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto dto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(dto);
            return Ok("Kategori başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Kategori başarıyla silindi.");
        }

        [HttpPut("{specialOfferId}")]
        public async Task<IActionResult> UpdateSpecialOffer(string specialOfferId, [FromBody] UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            updateSpecialOfferDto.SpecialOfferId = specialOfferId;

            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Kategori başarıyla güncellendi.");
        }
    }
}
