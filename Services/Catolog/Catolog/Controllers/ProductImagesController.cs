using Catolog.DTOs.ProductImagesDTOs;
using Catolog.Services.ProductImagesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> ProductImagesList()
        {
            var values = await _productImagesServices.GetAllProductImagesAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImagesById(string id)
        {
            var values = _productImagesServices.GetByIdProductImagesAsync(id);
            return Ok(values);
        }

        [HttpPost]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateProductImages(CreateProductImagesDTOs createProductImagesDTOs)
        {
            await _productImagesServices.CreateProductImagesAsync(createProductImagesDTOs);
            return Ok("Ürün görseli başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteProductImages(string id)
        {
            await _productImagesServices.DeleteProductImagesAsync(id);
            return Ok("Ürün görseli başarıyla silindi.");
        }

        [HttpPut]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateProductImages(UpdateProductImagesDTOs updateProductImagesDTOs)
        {
            await _productImagesServices.UpdateProductImagesAsync(updateProductImagesDTOs);
            return Ok("Ürün görseli başarıyla güncellendi.");
        }

        [HttpGet("GetProductImagesByProductId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImagesByProductId(string id)
        {
            var values = await _productImagesServices.GetProductImagesByProductIdAsync(id);
            return Ok(values);
        }
    }
}
