using IdentityServer.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Duende.IdentityServer.IdentityServerConstants;

namespace IdentityServer.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("statistics")]
        [AllowAnonymous] 
        public async Task<IActionResult> GetStatistics()
        {
            var userCount = await _userManager.Users.CountAsync();
            return Ok(new { TotalUserCount = userCount });
        }
    }
}
