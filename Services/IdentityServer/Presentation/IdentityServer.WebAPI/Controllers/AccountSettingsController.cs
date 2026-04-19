using IdentityServer.Application.Dtos;
using IdentityServer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountSettingsController : ControllerBase
    {
        private readonly IAccountSettingsService _accountSettingsService;

        public AccountSettingsController(IAccountSettingsService accountSettingsService)
        {
            _accountSettingsService = accountSettingsService;
        }

        [HttpPost("change-email")]
        //change email
        public async Task<IActionResult> ChangeEmail([FromBody] string email)
        {
            await _accountSettingsService.UpdateEmailAsync(email);
            return Ok();
        }

        //email onaylamada login gerekmez
        [AllowAnonymous]
        [HttpPost("confirm-email-change")]
        public async Task<IActionResult> ConfirmEmailChange([FromQuery] string userId, [FromQuery] string email, [FromQuery] string token)
        {
            await _accountSettingsService.ConfirmEmailChangeAsync(userId, email, token);
            return Ok();
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _accountSettingsService.GetUserAsync();
            return Ok(user);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            await _accountSettingsService.ChangePasswordAsync(dto);
            return Ok();
        }
    }
}
