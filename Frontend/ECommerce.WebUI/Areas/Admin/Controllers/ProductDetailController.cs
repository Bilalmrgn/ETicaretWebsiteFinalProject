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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProductDetail(string id)//id = product ın id si(productId)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var productResponse = await client.GetAsync($"https://localhost:7166/api/Product/GetProductById/{id}");


            if (productResponse.IsSuccessStatusCode)
            {
                var jsonData = await productResponse.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);

                //bastığım ürüne ait detayları getirmek için
                var productDetailResponse = await client.GetAsync($"api/ProductDetail/GetByProductId/{id}");

                if (productDetailResponse.IsSuccessStatusCode)
                {
                    var jsonDatas = await productDetailResponse.Content.ReadAsStringAsync();

                    // 2. Bu string'i DTO nesnesine dönüştür (Deserialize)
                    var detail = JsonConvert.DeserializeObject<ResultProductDetailDto>(jsonDatas);

                    // 3. Artık nesne üzerinden ID'ye erişebilirsin
                    ViewBag.ProductDetailId = detail?.ProductDetailId;
                    ViewBag.ExistingImage = detail;
                }

                return View(product);
            }

            return View();
        }

        //product detail post metod
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

           

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/ProductDetail/{dto.ProductId}",stringContent);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View();
        }
    }
}
