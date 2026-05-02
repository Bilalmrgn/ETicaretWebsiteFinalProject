using Frontend.DtosLayer.PaymentDtos.CreditCardDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminPaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminPaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");

            var response = await client.GetAsync("api/CreditCard");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonSerializer.Deserialize<List<ResultCreditCardDto>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(values ?? new List<ResultCreditCardDto>());
            }

            return View(new List<ResultCreditCardDto>());
        }

        public async Task<IActionResult> GetByUserName(string userId)
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");

            var response = await client.GetAsync($"api/CreditCard/ByUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonSerializer.Deserialize<List<ResultCreditCardDto>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View("Index", values ?? new List<ResultCreditCardDto>());
            }

            return View("Index", new List<ResultCreditCardDto>());
        }


    }
}
