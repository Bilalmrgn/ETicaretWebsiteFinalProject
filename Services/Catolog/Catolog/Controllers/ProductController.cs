using Catolog.DTOs.ProductDTOs;
using Catolog.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/Product
        // Tüm ürünler
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        // GET /api/Product/last10
        // Son 10 ürün
        [AllowAnonymous]
        [HttpGet("last10")]
        public async Task<IActionResult> GetLast10()
        {
            var values = await _productService.GetLast10ProductsAsync();
            return Ok(values);
        }

        // GET /api/Product/{id}
        // Id'ye göre ürün getir
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        // POST /api/Product
        // Yeni ürün ekle
        [HttpPost]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateProductDTOs createProductDTOs)
        {
            await _productService.CreateProductAsync(createProductDTOs);
            return Ok("Ürün başarıyla eklendi");
        }

        // DELETE /api/Product/{id}
        // Ürün sil
        [HttpDelete("{id}")]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        // PUT /api/Product/{id}
        // Ürün güncelle
        [HttpPut("{id}")]
        [Authorize(Policy = "CatalogWrite")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateProductDTOs updateProductDTOs)
        {
            updateProductDTOs.ProductId = id;
            await _productService.UpdateProductAsync(updateProductDTOs);
            return Ok("Ürün başarıyla güncellendi.");
        }

        // GET /api/Product/by-category/{categoryId}
        // Kategoriye göre ürünleri getir
        [AllowAnonymous]
        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(string categoryId)
        {
            var values = await _productService.GetProductsByCategoryIdAsync(categoryId);
            return Ok(values);
        }
    }
}
