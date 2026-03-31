using Frontend.DtosLayer.SliderFeatureDto;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminFeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminFeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //get all feature slider
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var response = await client.GetAsync("api/FeatureSlider");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);

                return View(values);
            }

            return View();

        }
        //create feature slider get method
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            return View();
        }

        //Create Feature slider post method
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");



            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/FeatureSlider", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFeatureSlider", new { area = "Admin" });
            }

            return View();
        }

        //delete feature slider 
        [HttpPost]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");



            var response = await client.DeleteAsync($"https://localhost:7166/api/FeatureSlider/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFeatureSlider", new { area = "Admin" });
            }

            return View();

        }

        //update feature slider get method
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

     

            var response = await client.GetAsync($"api/FeatureSlider/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //update feature slider
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

     

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/FeatureSlider/{dto.FeatureSliderId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFeatureSlider", new { area = "Admin" });

            }

            return View();
        }

    }
}
