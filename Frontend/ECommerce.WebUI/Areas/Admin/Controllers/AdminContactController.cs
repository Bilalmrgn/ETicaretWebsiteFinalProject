using Frontend.DtosLayer.ContactDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //get all contact message
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("ContactClient");

            var response = await client.GetAsync("api/Contact");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultContactDto>());
        }

        //get by id contact message
        public async Task<IActionResult> GetByIdContactMessage(int id)
        {
            var client = _httpClientFactory.CreateClient("ContactClient");

            var response = await client.GetAsync($"api/Contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<GetByIdContactDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //delete contanct message
        [HttpPost]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            var client = _httpClientFactory.CreateClient("ContactClient");

            var response = await client.DeleteAsync($"api/Contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Mesaj başarıyla silindi.";
                return RedirectToAction("Index", "AdminContact", new { Area = "Admin" });
            }

            return View();
        }
    }
}
