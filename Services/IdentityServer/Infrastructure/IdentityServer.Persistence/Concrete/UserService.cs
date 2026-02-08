using IdentityServer.Application.Dtos;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Persistence.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public Task<UserResponse> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> DeleteAccountAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Kullanıcı Bulunaadı.");
            }

            var result = await _userManager.DeleteAsync(user);

            UserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla silindi";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            }

            return response;

        }

        public Task<UserResponse> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse> RegisterAsync(RegisterDto registerDto)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                City = registerDto.City,
                PhoneNumber = registerDto.PhoneNumber
            }, registerDto.Password);

            UserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturuldu";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }
            }

            return response;

        }

        public Task<UserResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            throw new NotImplementedException();
        }
    }
}
