using Duende.IdentityServer.Services;
using IdentityServer.Domain;
using IdentityServer.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebAPI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _interaction = interaction;
        }

        [HttpGet]
        public async Task<IActionResult> LoginAsync(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_interaction.IsValidReturnUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            }
            // 1. Gelen returnUrl içindeki parametreleri çöz (IdentityServer interaction service kullanarak)
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

            // 2. Eðer return_to parametresi "register" ise direkt Register'a yolla
            if (context?.Parameters.Get("return_to") == "register")
            {
                return RedirectToAction("Index", "Register", new { returnUrl });
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // returnUrl varsa oraya, yoksa ana sayfaya yönlendir
                    if (_interaction.IsValidReturnUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("~/");
                }
            }

            ModelState.AddModelError("", "Geçersiz giriþ denemesi.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // 1. IdentityServer oturumunu (ASP.NET Identity üzerinden) kapat
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);

            if (string.IsNullOrEmpty(logoutRequest?.PostLogoutRedirectUri))
            {
                return Redirect("~/");
            }

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }
    }
}
