using Frontend.DtosLayer.AccountSettingsDtos;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ECommerce.WebUI.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class AccountSettingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountSettingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 🔹 Kullanıcı bilgisi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("IdentityClient");

                // Burası patlıyor olabilir (TokenHandler aşaması)
                var response = await client.GetAsync("auth/me");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"API Hatası (Ocelot/Identity): {response.StatusCode} - {errorContent}";
                    return RedirectToAction("Index", "Home");
                }

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json) || json.Trim().StartsWith("<"))
                {
                    TempData["Error"] = "API'den beklenen JSON verisi gelmedi (HTML döndü).";
                    return RedirectToAction("Index", "Home");
                }

                var user = JsonSerializer.Deserialize<GetUserDto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return View(user);
            }
            catch (Exception ex)
            {
                // Eğer hata Frontend içindeyse buraya düşecek
                TempData["Error"] = $"Frontend Hatası: {ex.Message} -> {ex.InnerException?.Message}";
                return RedirectToAction("Index", "Home");
            }
        }

        // 🔹 Email değiştir
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(string newEmail)
        {
            var client = _httpClientFactory.CreateClient("IdentityClient");

            var content = new StringContent(
                JsonSerializer.Serialize(newEmail),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("auth/change-email", content);

            if (response.IsSuccessStatusCode)
                TempData["Success"] = "Doğrulama maili gönderildi";
            else
                TempData["Error"] = "Email değiştirilemedi";

            return RedirectToAction("Index");
        }

        // Email confirm (mailden gelen link)
        [HttpGet]
        public async Task<IActionResult> ConfirmEmailChange(string userId, string email, string token)
        {
            var client = _httpClientFactory.CreateClient("IdentityClient");

            var response = await client.PostAsync(
                $"auth/confirm-email-change?userId={userId}&email={email}&token={token}",
                null);

            if (response.IsSuccessStatusCode)
                return View("EmailConfirmed");

            return View("Error");
        }

        // 🔹 Şifre değiştir
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var client = _httpClientFactory.CreateClient("IdentityClient");

            var content = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("auth/change-password", content);

            if (response.IsSuccessStatusCode)
                TempData["Success"] = "Şifre başarıyla değiştirildi.";
            else
                TempData["Error"] = "Şifre değiştirilemedi.";

            return RedirectToAction("Index");
        }

        // 🔹 Kullanıcı adı değiştir
        [HttpPost]
        public async Task<IActionResult> ChangeUsername(string newUsername)
        {
            var client = _httpClientFactory.CreateClient("IdentityClient");

            var content = new StringContent(
                JsonSerializer.Serialize(newUsername),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync("auth/change-username", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Kullanıcı adı başarıyla güncellendi.";
            }

            else
            {
                TempData["Error"] = "Kullanıcı adı değiştirilemedi.";
            }


            return RedirectToAction("Index");
        }
    }
}