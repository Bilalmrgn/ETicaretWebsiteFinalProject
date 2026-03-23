using Duende.IdentityModel.Client;
using Frontend.DtosLayer.AdminLoginDto;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
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
            return View();
        }

        //login post
        [HttpPost]
        public async Task<IActionResult> Index(AdminLoginDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            //yazdığım değerleri api'deki login kısmına gönder. Api'de kullanıcı var mı yok mu bunların kontrolü yapılacak
            var response = await client.PostAsync("https://localhost:7222/api/User/login", content);

            //response başarılı ise if içine gir
            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                //gelen veriyi json a çevir
                var userDetail = JsonConvert.DeserializeObject<AdminLoginDto>(responseContent);

                //kullanıcı admin ise identityserver dan scope ları al.
                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7222");
                
                if(disco.IsError)
                {
                    return View(dto);
                }

                //kullanıcının rolü null ise yada rolü admin değilse şifre doğrulaması yaparak uyar
                if(userDetail.Roles == null || !userDetail.Roles.Contains("Admin"))
                {
                    // Şifreyi doğrulamak için normal kullanıcı yetkisiyle token almayı dene
                    var checkToken = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                    {
                        Address = disco.TokenEndpoint,
                        ClientId = "ECommerceManagerId",
                        ClientSecret = "ecommercesecret",
                        UserName = dto.Email,
                        Password = dto.Password,
                        Scope = "catalog.full order.getAllOrder comment.full contact.create offline_access openid profile"
                    });
                    
                    if (checkToken.IsError)
                    {
                        ModelState.AddModelError("", "E-posta veya şifre hatalı.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bu panel sadece yöneticilere özeldir");
                    }
                    return View(dto);
                }

                

                
                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "ECommerceAdminId", // Config dosandaki Admin ClientId
                    ClientSecret = "ecommercesecret",
                    UserName = dto.Email,
                    Password = dto.Password,
                    Scope = "catalog.full order.full discount.full cargo.full basket.full comment.full contact.full offline_access openid profile email IdentityServerApi"
                });

                if(tokenResponse.IsError)
                {
                    ModelState.AddModelError("", "Yetkilendirme sunucusu hatası: " + tokenResponse.ErrorDescription);
                    return View(dto);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,dto.Email),
                    new Claim(ClaimTypes.Email, dto.Email),
                    new Claim("access_token", tokenResponse.AccessToken)
                };

                // Rollerimizi Claim listesine ekliyoruz ki [Authorize(Roles="Admin")] çalışsın
                foreach (var role in userDetail.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = dto.RememberMe
                };

                // Tokenları Cookie içinde saklıyoruz (API isteklerinde Header'a eklemek için)
                authProperties.StoreTokens(new List<AuthenticationToken>
                {
                    new AuthenticationToken { Name = "access_token", Value = tokenResponse.AccessToken },
                    new AuthenticationToken { Name = "refresh_token", Value = tokenResponse.RefreshToken },
                    new AuthenticationToken { Name = "expires_at", Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o") }
                });

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                // 4. Giriş başarılı, Admin Dashboard'a yönlendir
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });

            }

            ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "AdminLogin");
        }

    }
}
