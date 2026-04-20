using IdentityServer.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Interfaces
{
    public interface IAccountSettingsService
    {
        Task<GetUserDto> GetUserAsync();
        Task UpdateUserProfileAsync(UpdateUserProfileDto dto);
        Task ChangePasswordAsync(ChangePasswordDto dto);
        Task UpdatePhoneNumberAsync(string phoneNumber);
        Task UpdateEmailAsync(string email);
        Task ConfirmEmailChangeAsync(string userId, string newEmail, string token);
        Task UpdateUsernameAsync(string newUsername);
    }
}
