using IdentityServer.Application.Dtos;
using IdentityServer.Application.Exceptions;
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
        public async Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.(Persistence/Concrete/UserService/ChangePasswordAsync)");
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();

                throw new IdentityOperationException(errors);
            }
            return new UserResponse
            {
                Message = "Şifre başarıyla değiştirildi."
            };
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

            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new Exception("Şifreler eşleşmiyor");

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

        public async Task<ResetPasswordTokenResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            //1.kullanıcı email e göre bulunur
            var hasUser = await _userManager.FindByEmailAsync(resetPasswordDto.Email);

            if (hasUser == null)
            {
                throw new Exception("Bu email e sahip kullanıcı bulunamamıştır.(/Persistence/Concrete/Userservice/ResetPasswordAsync)");
            }

            // 2. bu emaile ait kullanıcı için token üretilir
            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            return new ResetPasswordTokenResult
            {
                UserId = hasUser.Id,
                Token = passwordResetToken,
            };
        }

        public async Task<UserResponse> UpdateUserProfile(string userId, UpdateUserProfileDto updateUserProfileDto)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.(/Identity/Persistence/Concrete/UserService/UpdateUserProfile)");


            // 🔹 SADECE alanları güncelle
            user.UserName = updateUserProfileDto.UserName;
            user.Email = updateUserProfileDto.Email;
            user.PhoneNumber = updateUserProfileDto.PhoneNumber;
            user.Name = updateUserProfileDto.Name;
            user.Surname = updateUserProfileDto.Surname;
            user.City = updateUserProfileDto.City;

            
            var result = await _userManager.UpdateAsync(user);

            return new UserResponse
            {
                Succeeded = result.Succeeded,
                Message = result.Succeeded
                    ? "Kullanıcı başarıyla güncellendi"
                    : string.Join("\n", result.Errors.Select(e => e.Description))
            };
        }
    }
}
