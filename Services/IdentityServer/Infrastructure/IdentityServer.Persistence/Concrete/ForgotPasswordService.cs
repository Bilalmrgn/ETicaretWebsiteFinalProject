using IdentityServer.Application.Interfaces;
using IdentityServer.Domain;
using IdentityServer.Infrastructure.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Persistence.Concrete
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public ForgotPasswordService(IEmailService emailService,UserManager<AppUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        /// <summary>
        /// amaç: token üret + mail gönder
        /// </summary>

        public async Task SendResetPasswordEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || !user.EmailConfirmed)
            {
                return;
            }

            //kullanıcıyı bulduk şimdi token üretmemiz gerekiyor
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            //link oluştur
            var link = $"https://localhost:7222/Account/ResetPassword?email={email}&token={encodedToken}";

            await _emailService.SendResetPasswordEmail(link, email);
        }



        /// <summary>
        /// amaç: token doğrula ve şifre değiştir
        /// </summary>

        public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

            return result.Succeeded;
        }



       
    }
}
