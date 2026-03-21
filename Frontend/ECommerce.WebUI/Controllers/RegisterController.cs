using Frontend.DtosLayer.RegisterDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //register get metod sayfa gösterimi için
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //register post metod
        public async Task<IActionResult> Index(RegisterDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("https://localhost:7222/api/User/register", stringContent);
        
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            // API'den gelen ham hata mesajını oku
            var errorDetail = await response.Content.ReadAsStringAsync();

            // Kullanıcıya daha anlamlı bir mesaj göstermek için basit bir kontrol:
            if (errorDetail.Contains("more than one element"))
            {
                ViewBag.ErrorMessage = "Sistemde bu e-posta ile kayıtlı birden fazla hesap bulundu. Lütfen destekle iletişime geçin.";
            }
            else
            {
                ViewBag.ErrorMessage = "Giriş yapılamadı. E-posta veya şifre hatalı.";
            }

            return View(dto);

        }
    }
}
