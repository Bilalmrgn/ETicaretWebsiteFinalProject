using IdentityServer.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(RegisterDto registerDto);
        Task<UserResponse> LoginAsync(LoginDto loginDto);
        Task<UserResponse> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto);
        Task<ResetPasswordTokenResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<UserResponse> DeleteAccountAsync(string userId);
        //Task SendResetPasswordLinkAsync(string email);
        Task<UserResponse> UpdateUserProfile(string userId, UpdateUserProfileDto updateUserProfileDto);
    }
}
