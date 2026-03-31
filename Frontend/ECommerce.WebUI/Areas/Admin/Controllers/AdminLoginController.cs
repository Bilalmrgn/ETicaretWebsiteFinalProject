using Duende.IdentityModel.Client;
using Frontend.DtosLayer.AdminLoginDto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    //grant type authorization code a göre login işlemi

    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminLoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }

            // Challenge metodu, Program.cs'deki OpenIdConnect ayarlarını tetikler 
            // ve kullanıcıyı otomatik olarak IdentityServer Login ekranına fırlatır.
            return Challenge(new AuthenticationProperties
            {
                RedirectUri = "/Admin/AdminHome/Index"
            }, OpenIdConnectDefaults.AuthenticationScheme);
        }

        //Logout
        public async Task<IActionResult> LogOut()
        {
            //grant type authorization code kullandığımızdan dolayı logout durumunda hem kendi uygulamandaki(cookie) hem de identityserverdaki (oidc) oturumu kapat
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "AdminLogin");
        }

        //not: authorization grant type kullandığımızdan dolayı login işleminde post metodu kullanmamıza gerek kalmıyor

    }
}
