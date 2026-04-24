using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Application.Interfaces
{
    public interface IForgotPasswordService
    {
        Task SendResetPasswordEmailAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
