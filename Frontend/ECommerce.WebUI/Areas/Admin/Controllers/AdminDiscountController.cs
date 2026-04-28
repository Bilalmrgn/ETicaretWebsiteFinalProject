using Frontend.DtosLayer.DiscountDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class AdminDiscountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDiscountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 🔹 GET ALL
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("DiscountClient");

            var response = await client.GetAsync("/discount");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCouponDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultCouponDto>());
        }

        // 🔹 CREATE GET
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        // 🔹 CREATE POST
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateCoupontDto dto)
        {
            var client = _httpClientFactory.CreateClient("DiscountClient");

            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/discount", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Discounts", new { area = "Admin" });
            }

            return View(dto);
        }

        // 🔹 DELETE
        [HttpPost]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var client = _httpClientFactory.CreateClient("DiscountClient");

            await client.DeleteAsync($"/discount/{id}");

            return RedirectToAction("Index", "Discounts", new { area = "Admin" });
        }

        // 🔹 UPDATE GET
        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            var client = _httpClientFactory.CreateClient("DiscountClient");

            var response = await client.GetAsync($"/discount/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCouponDto>(jsonData);

                return View(values);
            }

            return View();
        }

        // 🔹 UPDATE POST
        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateCouponDto dto)
        {
            var client = _httpClientFactory.CreateClient("DiscountClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/discount/{dto.CouponId}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Discounts", new { area = "Admin" });
            }

            return View(dto);
        }
    }
}
