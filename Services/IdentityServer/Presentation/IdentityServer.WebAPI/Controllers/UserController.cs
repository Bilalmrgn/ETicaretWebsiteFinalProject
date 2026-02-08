using IdentityServer.Application.Dtos;
using IdentityServer.Application.Interfaces;
using IdentityServer.Infrastructure.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        public UserController(IUserService userService,IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        //Create User
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterDto dto)
        {
            var result = await _userService.RegisterAsync(dto);

            return Ok(result);
        }

        //Delete User
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteAccountAsync(userId);

            return Ok(result);
        }

        [HttpPost("change-password")]//url de change password yazar
        public async Task<IActionResult> ChangePassword(string userId, ChangePasswordDto dto)
        {
            var result = await _userService.ChangePasswordAsync(userId, dto);

            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _userService.ResetPasswordAsync(dto);

            var baseUrl =
                $"{Request.Scheme}://{Request.Host}";

            var resetLink =
                $"{baseUrl}/reset-password" +
                $"?userId={result.UserId}&token={Uri.EscapeDataString(result.Token)}";

            await _emailService.SendResetPasswordEmail(resetLink, dto.Email);

            return Ok();
        }
    }
}
