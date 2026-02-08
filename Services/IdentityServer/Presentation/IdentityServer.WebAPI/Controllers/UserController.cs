using IdentityServer.Application.Dtos;
using IdentityServer.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
    }
}
