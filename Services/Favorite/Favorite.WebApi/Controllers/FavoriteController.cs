using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Favorite.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        public async Task<IActionResult> AddProduct()
        {
            return Ok();
        }

        public async Task<IActionResult> DeleteProduct()
        {
            return Ok();
        }

        public async Task<IActionResult> ListProducts()
        {
            return Ok();
        }
    }
}
