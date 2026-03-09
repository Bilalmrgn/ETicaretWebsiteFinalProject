using Catolog.DTOs.CategoryDTOs;
using Catolog.DTOs.FeatureSliderDto;
using Catolog.Services.CategoryServices;
using Catolog.Services.FeatureSliderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSliderController : ControllerBase
    {
        private readonly IFeatureSliderService _featureSliderService;
        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        // kategori listesi
        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id)
        {
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto dto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(dto);
            return Ok("Kategori başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Kategori başarıyla silindi.");
        }

        [HttpPut("{featureSliderId}")]
        public async Task<IActionResult> UpdateFeatureSlider(string featureSliderId, [FromBody] UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            updateFeatureSliderDto.FeatureSliderId = featureSliderId;

            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Kategori başarıyla güncellendi.");
        }
    }
}
