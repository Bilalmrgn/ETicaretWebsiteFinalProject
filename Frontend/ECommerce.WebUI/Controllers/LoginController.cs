using Frontend.DtosLayer.LoginDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text;
using Duende.IdentityModel.Client;

namespace ECommerce.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //login get metod sayfa gösterilmesi için
        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, "oidc");
        }

        //login post metod
        [HttpPost]
        public IActionResult Index(LoginDto dto)
        {
            // Challenge metodu, Program.cs'deki OpenIdConnect konfigürasyonunu tetikler.
            // Bu işlem kullanıcıyı IdentityServer'ın login sayfasına yönlendirir.
            var properties = new AuthenticationProperties
            {
                RedirectUri = "/", 
                IsPersistent = dto.RememberMe
            };

            // "OpenIdConnectDefaults.AuthenticationScheme" veya senin Program.cs'de verdiğin isim
            return Challenge(properties, "oidc");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/" // IdentityServer'dan dönünce buraya gelecek
            }, "Cookies", "oidc");
        }
    }
}
