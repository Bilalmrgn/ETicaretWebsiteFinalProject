using Favorite.WebApi.Context;
using Favorite.WebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Favorite.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FavoriteController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite([FromBody] string productId)
        {
            //kullanıcının id sini al
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //bu id ye sahip ürünün favorilerde olup olmadığını kontrol et
            var exist = _context.Favorites.Any(f => f.UserId == userId && f.ProductId == productId);

            if (exist)
            {
                return BadRequest("Bu ürün zaten favorilerinizde.");
            }

            var favorite = new FavoriteModel
            {
                UserId = userId,
                ProductId = productId
            };

            _context.Favorites.Add(favorite);

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteFavorite(string productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var item = _context.Favorites.FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if(item == null)
            {
                return NotFound("Bu ürün favorilerinizde bulunamadı.");
            }

            _context.Favorites.Remove(item);
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListFavorites()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var favorites = _context.Favorites.Where(f=>f.UserId == userId).ToList();

            return Ok(favorites);
            
        }
    }
}
