using Frontend.DtosLayer.ProductDetailDto;
using Frontend.DtosLayer.ProductImageDto;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // Ürün Detay Listesi (Index)
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync("/catalog/ProductDetail");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ProductDetailDto>>(jsonData);

                return View(values);
            }
            return View(new List<ProductDetailDto>()); // Model null gitmesin diye boş liste
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)//id = product ın id si(productId)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            // 1. Ürün bilgilerini getir (Adı vb. için)
            var productResponse = await client.GetAsync($"/catalog/product/{id}");


            if (productResponse.IsSuccessStatusCode)
            {
                var jsonData = await productResponse.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);

                // 2. Ürüne ait detayı getir (Açıklama, Bilgi vb. için)
                var productDetailResponse = await client.GetAsync($"/catalog/ProductDetail/GetByProductId/{id}");

                if (productDetailResponse.IsSuccessStatusCode)
                {
                    var jsonDatas = await productDetailResponse.Content.ReadAsStringAsync();

                    var detail = JsonConvert.DeserializeObject<ResultProductDetailDto>(jsonDatas);

                    ViewBag.ProductDetailId = detail?.ProductDetailId;
                    ViewBag.ExistingImage = detail;
                }

                return View(product);
            }

            return View();
        }

        //product detail post metod
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var jsonData = System.Text.Json.JsonSerializer.Serialize(dto, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/catalog/ProductDetail/{dto.ProductId}", stringContent);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View();
        }
    }
}
