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
        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProduct()
        {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetLast10Products")]
        public async Task<IActionResult> GetLast10Product()
        {
            var values = await _productService.GetLast10ProductsAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTOs createProductDTOs)
        {
            await _productService.CreateProductAsync(createProductDTOs);
            return Ok("Ürün başarıyla eklendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProruct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody]UpdateProductDTOs updateProductDTOs)
        {
            updateProductDTOs.ProductId = id;
            await _productService.UpdateProductAsync(updateProductDTOs);
            return Ok("Ürün başarıyla güncellendi.");
        }

        //get product by category id
        [HttpGet("GetProductsByCategoryId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategoryId(string id)
        {
            var values = await _productService.GetProductsByCategoryIdAsync(id);
            return Ok(values);
        }
    }
}
