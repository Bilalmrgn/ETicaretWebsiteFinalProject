using ECommerce.WebUI.Services;
using Frontend.DtosLayer.CategoryDto;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public ProductsController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

    
            
            var response = await client.GetAsync("api/Product/GetAllProducts");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                return View(values);
            }

            return View(new List<ProductListDto>());
        }

        //Create Product get
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

    

            var response = await client.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                //listeleme işlemlerinde deserialize
                var values = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);

                ViewBag.categories = values;
            }
            return View();
        }

        //Create Product post
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            //api ye istek göndermem için bir HTTPClient nesnesi oluşturdum
            var client = _httpClientFactory.CreateClient("CatalogClient");       

            //ekleme ve güncelleme işlemlerinde serialize
            var jsonData = JsonConvert.SerializeObject(dto);

            //json verisini http içeriğine çevirme
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Product", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View(dto);
        }

        //Delete product
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.DeleteAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Products", new { area = "Admin" });
        }

        //Update product
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            //header da token gönderdikten sonra verimizi serialize şeklinde ileteceğiz. çünkü ekleme ve güncellemede serialize
            var response = await client.GetAsync($"api/Product/GetProductById/{id}");

            if (response.IsSuccessStatusCode) //405 hata kodu alıyorum buradan devam edeceğim
            {
                //Http'den gelen içeriği string olarak oku
                var jsonData = await response.Content.ReadAsStringAsync();

                //gelen json datayı deserialize et çünkü bu listeleme işlemidir
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);

                //kategori değiştirebilmek için kategorileri listeleme
                var categoryResponse = await client.GetAsync("api/Categories");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

                    var categoryList = JsonConvert.DeserializeObject<List<CategoryListDto>>(categoryJson);

                    ViewBag.categories = categoryList;
                }
                else
                {
                    ViewBag.categories = new List<CategoryListDto>();
                }
                return View(values);
            }

            return View();

        }

        //Update Product Post
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            //string veriyi Http içerisine koymamız gerekiyor
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PutAsync($"api/Product/{dto.ProductId}",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            // ❗ tekrar kategori yükle
            var categoryResponse = await client.GetAsync("api/Categories");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<CategoryListDto>>(categoryJson);

                ViewBag.categories = categoryList;
            }

            return View(dto);
        }


    }
}
