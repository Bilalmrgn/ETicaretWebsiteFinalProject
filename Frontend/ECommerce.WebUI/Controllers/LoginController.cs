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
            return View();
        }

        //login post metod
        [HttpPost]
        public async Task<IActionResult> Index(LoginDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //api ye git ve bu kullanıcı var mı ve rolü ne kontrolü yap
            var response = await client.PostAsync("https://localhost:7222/api/User/login", content);

            if (response.IsSuccessStatusCode)
            {
                //api'den gelen kullanıcı detaylarını oku
                var responseConent = await response.Content.ReadAsStringAsync();

                var userDetail = JsonConvert.DeserializeObject<LoginDto>(responseConent);

                //varsayılan kullanıcı
                string clientId = "ECommerceManagerId";
                string scopes = "catalog.full order.getAllOrder comment.full contact.create offline_access openid profile";

                //api'den gelen roller içinde "admin" varsa admin'e yükselticez
                if (userDetail.Roles != null && userDetail.Roles.Contains("Admin"))
                {
                    clientId = "ECommerceAdminId";
                    scopes = "catalog.full order.full discount.full cargo.full basket.full comment.full contact.full offline_access openid profile email";
                }

                //identity server dan login olunca token iste
                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7222");
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = clientId,
                    ClientSecret = "ecommercesecret",
                    UserName = dto.Email,
                    Password = dto.Password,
                    Scope = scopes
                });
                if (tokenResponse.IsError)
                {
                    // Buraya breakpoint koyun ve 'tokenResponse.ErrorDescription' içeriğine bakın.
                    // Muhtemelen "invalid_scope" hatası alıyorsunuz.
                    var error = tokenResponse.Error;
                    return View(dto);
                }
                //burası kullanıcı giriş yaptığında sağ üst top bar kısmında kullanıcı adını göstermek için
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, dto.Email),
                    new Claim(ClaimTypes.Email, dto.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe
                };

                authProperties.StoreTokens(new List<AuthenticationToken>
                {
                    new AuthenticationToken { Name = "access_token", Value = tokenResponse.AccessToken },
                    new AuthenticationToken { Name = "refresh_token", Value = tokenResponse.RefreshToken },
                    new AuthenticationToken { Name = "expires_at", Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o") }
                });


                //SignInAsync yaptık çünkü giriş yaptıktan sonra oturumun açık kalmasını istiyorum
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(dto);


        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}
