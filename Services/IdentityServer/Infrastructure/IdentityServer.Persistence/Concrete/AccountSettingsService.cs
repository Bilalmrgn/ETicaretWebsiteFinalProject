using IdentityServer.Application.Dtos;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain;
using IdentityServer.Infrastructure.EmailService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Persistence.Concrete
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public AccountSettingsService(IEmailService emailService,UserManager<AppUser> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _emailService = emailService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetUserDto> GetUserAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            return new GetUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Name = user.Name,
                Surname = user.Surname
            };
        }

        public Task UpdateUserProfileAsync(UpdateUserProfileDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            //1.login olan kullanıcı alınır
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            //2.şifre değiştir
            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public Task UpdatePhoneNumberAsync(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateEmailAsync(string email)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            //token üretilr ve mail e gönderilir
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);

            var encodedToken = WebUtility.UrlEncode(token);

            var link = $"https://localhost:7145/confirm-email-change?userId={user.Id}&email={email}&token={encodedToken}";

            await _emailService.SendEmailConfirmationEmail(link, email);
        }

        public async Task ConfirmEmailChangeAsync(string userId, string newEmail, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var decodedToken = WebUtility.UrlDecode(token);

            var result = await _userManager.ChangeEmailAsync(user, newEmail, decodedToken);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}