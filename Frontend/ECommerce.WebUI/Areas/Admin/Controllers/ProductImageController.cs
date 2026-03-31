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
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        //create product images get metod
        [HttpGet]
        public async Task<IActionResult> CreateProductImage(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var productResponse = await client.GetAsync($"api/Product/GetProductById/{id}");


            if (productResponse.IsSuccessStatusCode)
            {
                var jsonData = await productResponse.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);

                var imageResponse = await client.GetAsync($"api/ProductImages/GetProductImagesByProductId/{id}");

                if(imageResponse.IsSuccessStatusCode)
                {
                    var imagesData = await imageResponse.Content.ReadAsStringAsync();
                    ViewBag.ExistingImages = JsonConvert.DeserializeObject<List<ResultProductImageDto>>(imagesData); 
                }

                return View(product);
            }

            return View();
        }

        //create product images post metod
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");
         

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("/api/ProductImages", stringContent);

            if (response.IsSuccessStatusCode)
            {

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View();
        }

        //Delete product Image 
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var response = await client.DeleteAsync($"api/ProductImages/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View();
        }

    }
}
