using Frontend.DtosLayer.BasketDtos;
using Frontend.DtosLayer.PaymentDtos;
using Frontend.DtosLayer.PaymentDtos.CreditCardDtos;
using Frontend.DtosLayer.AccountSettingsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class PaymentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PaymentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int orderId)
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");
            var basketClient = _httpClientFactory.CreateClient("BasketClient");
            var identityClient = _httpClientFactory.CreateClient("IdentityClient");

            var response = await client.GetAsync("api/CreditCard");

            var json = await response.Content.ReadAsStringAsync();
            
            var cards = JsonConvert.DeserializeObject<List<ResultCreditCardDto>>(json);

            // Sepet tutarını çek
            var basketResponse = await basketClient.GetAsync("api/Basket");
            if (basketResponse.IsSuccessStatusCode)
            {
                var basketJson = await basketResponse.Content.ReadAsStringAsync();
                var basket = JsonConvert.DeserializeObject<BasketTotalDto>(basketJson);
                ViewBag.TotalPrice = basket?.TotalPrice ?? 0;
            }

            // Identity API'den kullanıcı bilgisini çek (Claim'de yoksa en garanti yol)
            var identityResponse = await identityClient.GetAsync("auth/me");
            if (identityResponse.IsSuccessStatusCode)
            {
                var identityJson = await identityResponse.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<GetUserDto>(identityJson);
                ViewBag.Email = user?.Email;
            }

            ViewBag.OrderId = orderId;
            
            return View(cards);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentRequestDto dto)
        {
            var client = _httpClientFactory.CreateClient("PaymentClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Payment", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AccountSettings");
            }

            ModelState.AddModelError("", "Ödeme başarısız");

            // eğer response başarısızsa tekrar kartları yükle
            var cardsResponse = await client.GetAsync("api/CreditCard");

            var cardsJson = await cardsResponse.Content.ReadAsStringAsync();

            var cards = JsonConvert.DeserializeObject<List<ResultCreditCardDto>>(cardsJson);

            return View(cards);
        }
    }
}
