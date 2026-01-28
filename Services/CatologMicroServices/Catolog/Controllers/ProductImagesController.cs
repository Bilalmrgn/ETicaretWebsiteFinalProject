using Catolog.DTOs.ProductImagesDTOs;
using Catolog.Services.ProductImagesServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImagesServices;
        public ProductImagesController(IProductImageService productImagesServices)
        {
            _productImagesServices = productImagesServices;
        }

        // kategori listesi
        [HttpGet]
        public async Task<IActionResult> ProductImagesList()
        {
            var values = await _productImagesServices.GetAllProductImagesAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImagesById(string id)
        {
            var values = _productImagesServices.GetByIdProductImagesAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImages(CreateProductImagesDTOs createProductImagesDTOs)
        {
            await _productImagesServices.CreateProductImagesAsync(createProductImagesDTOs);
            return Ok("Ürün görseli başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductImages(string id)
        {
            await _productImagesServices.DeleteProductImagesAsync(id);
            return Ok("Ürün görseli başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductImages(UpdateProductImagesDTOs updateProductImagesDTOs)
        {
            await _productImagesServices.UpdateProductImagesAsync(updateProductImagesDTOs);
            return Ok("Ürün görseli başarıyla güncellendi.");
        }
    }
}
