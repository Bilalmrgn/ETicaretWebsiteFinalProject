using ECommerce.WebUI.Services;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public ProductDetailController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        //get all product detail
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "";

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7166/api/ProductDetail");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<ProductDetailDto>(jsonData);

                return View(values);
            }

            return View();
        }

        /*//Create Product Detail Get
        public IActionResult Details()
        {

        }*/
    }
}
