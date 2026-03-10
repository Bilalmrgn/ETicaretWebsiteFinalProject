using Catolog.DTOs.BrandDto;
using Catolog.Services.BrandService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        //Get all brand with get metod
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var values = await _brandService.GetAllBrandsAsync();

            return Ok(values);
        }

        //get by id brand get metod
        [HttpGet("{BrandId}")]
        public async Task<IActionResult> GetByIdBrand(string BrandId)
        {
            var value = await _brandService.GetByIdBrandAsync(BrandId);

            return Ok(value);
        }

        //Create brand
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto dto)
        {
            await _brandService.CreateBrandAsync(dto);

            return Ok("Marka başarıyla oluşturuldu");
        }

        //Update brand
        [HttpPut("{BrandId}")]
        public async Task<IActionResult> UpdateBrand(string BrandId, UpdateBrandDto dto)
        {
            dto.BrandId = BrandId;

            await _brandService.UpdateBrandAsync(dto);
            
            return Ok("Brand başarıyla güncellendi");
        }

        //delete brand
        [HttpDelete("{BrandId}")]
        public async Task<IActionResult> DeleteBrand(string BrandId)
        {
            await _brandService.DeleteBrandAsync(BrandId);

            return Ok("Brand Başarıyla Silindi");
        }
    }
}
