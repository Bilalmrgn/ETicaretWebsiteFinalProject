using Duende.IdentityServer.Services;
using IdentityServer.Application.Dtos;
using IdentityServer.Domain;
using IdentityServer.Infrastructure.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.VisualBasic;
using System.Text;

namespace IdentityServer.WebAPI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IEmailService _emailService;

        // Constructor'ları birleştirdik ve IdentityServer etkileşim servisini ekledik
        public RegisterController(
            IEmailService emailService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }
        [HttpGet]
        public IActionResult Index(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] RegisterDto dto, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        // Visual Studio 'Output' penceresine yazdırır
                        System.Diagnostics.Debug.WriteLine($"HATA -> Alan: {modelStateKey}, Sebep: {error.ErrorMessage}");

                        // Arayüzde (View) en üstte toplu görmek için:
                        ModelState.AddModelError("", $"{modelStateKey}: {error.ErrorMessage}");
                    }
                }
                return View(dto);
            }

            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Name = dto.Name,
                Surname = dto.Surname,
                City = dto.City ?? "Null" 
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                
                await _userManager.AddToRoleAsync(user, "Customer");

                //token üretilir
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                //email onayı için link oluştur (url action link oluşturur) - returnUrl çıkarıldı (bozulmayı önlemek için)
                var link = Url.Action("ConfirmEmail", "Register", new { userId = user.Id, token = encodedToken }, Request.Scheme);

                await _emailService.SendEmailConfirmationEmail(link!, user.Email);
                await _signInManager.SignOutAsync(); // Eski oturum kalıntılarını temizle
                return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });

            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(dto);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) return View("Error");

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if(result.Succeeded)
            {
                // Onay başarılıysa doğrudan Frontend (WebUI) sitesinin Login sayfasına yönlendiriyoruz.
                return Redirect("https://localhost:7145/Login/Index");
            }

            return View("Error");
        }
    }
}