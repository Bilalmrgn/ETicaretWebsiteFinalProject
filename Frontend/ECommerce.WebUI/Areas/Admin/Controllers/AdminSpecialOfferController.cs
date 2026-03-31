using Frontend.DtosLayer.SpecialOfferDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminSpecialOfferController : Controller
    {
        
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminSpecialOfferController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //List all Special offer
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var response = await client.GetAsync("api/SpecialOffer");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        //create special offer
        public IActionResult CreateSpecialOffer()
        {
            return View();
        }

        //create special offer Post method
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

         

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/SpecialOffer",stringContent);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });
            }

            return View();
        }

        //update special offer get method
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");



            var response = await client.GetAsync($"api/SpecialOffer/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //update special offer post metod
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto dto)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");



            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PutAsync($"api/SpecialOffer/{dto.SpecialOfferId}",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });
            }

            return View();
        }

        //delete special offer post method
        [HttpPost]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var response = await client.DeleteAsync($"api/SpecialOffer/{id}");

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });

            }

            return View();
        }
    }
}
