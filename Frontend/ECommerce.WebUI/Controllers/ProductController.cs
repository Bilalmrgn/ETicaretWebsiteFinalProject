using ECommerce.WebUI.Services;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public ProductController(IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
        }
        //AllProduct
        public IActionResult GetAllProduct()
        {
            return View();
        }

        //product detail
        public async Task<IActionResult> Details(string id)
        {

            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync($"api/Product/GetProductById/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        //kategoriye göre ürünleri listeleme
        public async Task<IActionResult> GetProductsByCategoryId(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient"); 

            var response = await client.GetAsync($"api/Product/GetProductsByCategoryId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);
                return View(values);
            }
            return View(new List<ProductListDto>());
      
        }
    }
}
