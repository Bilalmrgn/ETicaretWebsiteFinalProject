using Frontend.DtosLayer.BrandDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminBrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminBrandController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //Get all brand
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync("/catalog/brand");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultBrandDto>());
        }

        //Create brand get method
        [HttpGet]
        public IActionResult CreateBrand()
        {
            return View();
        }

        //Create brand post method
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/catalog/brand", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBrand", new { Area = "Admin" });
            }

            return View();


        }

        //update method with get metod
        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync($"/catalog/brand/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<UpdateBrandDto>(jsonData);

                return View(value);
            }
            return View();
        }

        //update method post metod
        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/catalog/brand/{dto.BrandId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBrand", new { Area = "Admin" });
            }

            return View();

        }

        //Delete method
        [HttpPost]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.DeleteAsync($"/catalog/brand/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminBrand", new { Area = "Admin" });

            }

            return View();

        }

    }
}
