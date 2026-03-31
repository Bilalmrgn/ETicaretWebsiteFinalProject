using Duende.IdentityServer.Services;
using IdentityServer.Application.Dtos;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace IdentityServer.WebAPI.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        // Constructor'ları birleştirdik ve IdentityServer etkileşim servisini ekledik
        public RegisterController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }
        [HttpGet]
        public IActionResult Index(string? originalReturnUrl)
        {
            ViewBag.ReturnUrl = originalReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] RegisterDto dto, [FromQuery] string? returnUrl)
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
                City = dto.City ?? "Null" // veya default bir değer
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                // 1. Önce rolü ata (DB'de 'Customer' rolü olduğundan emin ol)
                await _userManager.AddToRoleAsync(user, "Customer");

                // 2. Kullanıcıyı otomatik login yap (Opsiyonel, IdentityServer akışı için iyidir)
                await _signInManager.SignInAsync(user, isPersistent: false);

                // 3. KRİTİK NOKTA: Frontend'e geri gönderiş (Login'deki mantık)
                if (!string.IsNullOrEmpty(returnUrl) && _interaction.IsValidReturnUrl(returnUrl)) 
                {
                    // Bu satır kullanıcıyı frontend uygulamasındaki (React/Angular) 
                    // callback sayfasına (signin-oidc) geri fırlatır.
                    return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
                }

                // Eğer returnUrl yoksa, 404 almamak için güvenli bir yere yolla
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(dto);
        }
    }
}
