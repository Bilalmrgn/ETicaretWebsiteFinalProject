using IdentityServer.Application.Dtos;
using IdentityServer.Application.Exceptions;
using IdentityServer.Application.Interfaces;
using IdentityServer.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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

        //GetAllUser
        public async Task<List<UserListDto>> GetAllAsync()
        {
            var users = await _userManager.Users.Select(x => new UserListDto
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                City = x.City
            }).ToListAsync();

            if (users == null)
            {
                throw new Exception("Kullanıcılar Listelenemedi. (Identity/Persistence/UserService/GetAllAsync)");
            }

            return users;
        }

        //Login işlemleri
        public async Task<UserResponse> LoginAsync(LoginDto loginDto)
        {
            //1. kullanıcıyı bul
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return new UserResponse { Succeeded = false, Message = "Kullanıcı bulunamadı." };
            }

            //2. şifre doğrulanır
            var isPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPassword)
            {
                return new UserResponse { Succeeded = false, Message = "Email veya şifre hatalı." };
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return new UserResponse
            {
                Succeeded = true,
                Message = "Giriş Başarılı",
                Email = user.Email,
                Roles = userRoles.ToList()
            };

        }

        public async Task<UserResponse> RegisterAsync(RegisterDto registerDto)
        {

            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new Exception("Şifreler eşleşmiyor");

            var user = new AppUser
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                City = registerDto.City,
                PhoneNumber = registerDto.PhoneNumber

            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                // yeni kayıt olan herkes normal user olmasını istiyorum. bunun token e eklenmesini sağlayan sınıf ise customProfileServis kısmıdır
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", "User"));

                return new UserResponse { Succeeded = true, Message = "Kullanıcı başarıyla oluşturuldu" };
            }

            return new UserResponse
            {
                Succeeded = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };

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


       
            user.UserName = updateUserProfileDto.UserName;
            user.Email = updateUserProfileDto.Email;
            user.PhoneNumber = updateUserProfileDto.PhoneNumber;
            user.Name = updateUserProfileDto.Name;
            user.Surname = updateUserProfileDto.Surname;
            user.City = updateUserProfileDto.City;


            var result = await _userManager.UpdateAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);

            return new UserResponse
            {
                Succeeded = true,
                Message = "Giriş Başarılı",
                Email = user.Email, // Response nesnesinde bu alanların olduğundan emin ol
                Roles = userRoles.ToList() // List<string> olarak rolleri gönderiyoruz
            };
        }
    }
}
