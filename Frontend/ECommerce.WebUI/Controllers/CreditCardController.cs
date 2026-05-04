using Frontend.DtosLayer.PaymentDtos.CreditCardDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ECommerce.WebUI.Controllers
{

    public class CreditCardController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreditCardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");
            var response = await client.GetAsync("api/CreditCard/ByUserId");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(jsonData) && !jsonData.Trim().StartsWith("<"))
                {
                    var values = JsonSerializer.Deserialize<List<ResultCreditCardDto>>(jsonData, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return View(values ?? new List<ResultCreditCardDto>());
                }
            }

            return View(new List<ResultCreditCardDto>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCreditCard(CreateCreditCardDto dto)
        {

            var client = _httpClientFactory.CreateClient("PaymentClient");
            var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/CreditCard", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Kart başarıyla eklendi.";
                return RedirectToAction("Index");
            }


            TempData["Error"] = "Kart eklenemedi.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCreditCard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCreditCard(UpdateCreditCardDto dto)
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");

            var jsonData = JsonSerializer.Serialize(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/CreditCard/{dto.CreditCardId}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["UpdateSuccess"] = "Kart başarıyla güncellendi";
                return RedirectToAction("Index");
            }
            TempData["UpdateError"] = "Kart güncellenemedi";

            return RedirectToAction("Index");
        }
    }
}
