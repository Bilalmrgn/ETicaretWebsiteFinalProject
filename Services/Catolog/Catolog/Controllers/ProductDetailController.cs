using Catolog.DTOs.ProductDetailDTOs;
using Catolog.Services.ProductDetailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private readonly IProductDetailServices _productDetailServices;
        public ProductDetailController(IProductDetailServices productDetailServices)
        {
            _productDetailServices = productDetailServices;
        }

        // kategori listesi
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await _productDetailServices.GetAllProductDetailAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var values = await _productDetailServices.GetByIdProductDetailAsync(id);

            return Ok(values);
        }

        [HttpPost]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTOs createProductDetailDTOs)
        {
            await _productDetailServices.CreateProductDetailAsync(createProductDetailDTOs);
            return Ok("Ürün detayı başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailServices.DeleteProductDetailAsync(id);
            return Ok("Ürün detayı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateProductDetail([FromRoute] string id, [FromBody] UpdateProductDetailDTOs updateProductDetailDTOs)
        {
            updateProductDetailDTOs.ProductId = id;

            await _productDetailServices.UpdateProductDetailAsync(updateProductDetailDTOs);
            return Ok("Ürün detayı başarıyla güncellendi.");
        }

        [AllowAnonymous]
        [HttpGet("GetByProductId/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            //id = productId
            var value = await _productDetailServices.GetProductByIdAsync(id);
            return Ok(value);
        }
    }
}

