using Catolog.DTOs.ProductDTOs;
using Catolog.Services.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var values = await _productService.GetAllProductAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var values = _productService.GetByIdProductAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTOs createProductDTOs)
        {
            await _productService.CreateProductAsync(createProductDTOs);
            return Ok("Ürün başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProruct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok("Ürün başarıyla silindi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTOs updateProductDTOs)
        {
            await _productService.UpdateProductAsync(updateProductDTOs);
            return Ok("Ürün başarıyla güncellendi.");
        }

    }
}
