using Catolog.DTOs.ProductDetailDTOs;
using Catolog.Services.ProductDetailServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catolog.Controllers
{
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
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await _productDetailServices.GetAllProductDetailAsync();
            return Ok(values);
        }

        //id ye göre kategori listesi
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var values = _productDetailServices.GetByIdProductDetailAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDTOs createProductDetailDTOs)
        {
            await _productDetailServices.CreateProductDetailAsync(createProductDetailDTOs);
            return Ok("Ürün detayı başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await _productDetailServices.DeleteProductDetailAsync(id);
            return Ok("Ürün detayı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDTOs updateProductDetailDTOs)
        {
            await _productDetailServices.UpdateProductDetailAsync(updateProductDetailDTOs);
            return Ok("Ürün detayı başarıyla güncellendi.");
        }
    }
}

