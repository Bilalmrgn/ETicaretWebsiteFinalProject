using Basket.Dtos;
using Basket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [Authorize(Policy = "BasketFullPermission")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketController(IBasketService basketService, ILoginService loginService)
        {
            _basketService = basketService;
            _loginService = loginService;
        }

        //Get All Basket
        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var user = User.Claims;//sisteme girmiş olan token'ımızın içerisinde olan bilgileri getirir
            var values = await _basketService.GetBasketAsync(_loginService.GetUserId);

            return Ok(values);
        }

        //Save Basket (Create Or Update)
        [HttpPost]
        public async Task<IActionResult> SaveBasket(BasketTotalDto dto)
        {
            dto.UserId = _loginService.GetUserId;
            
            await _basketService.SaveAsync(dto);

            return Ok("Degisiklikler kaydedildi.");
        }

        //Delete Basket
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            await _basketService.DeleteAsync(_loginService.GetUserId);

            return Ok("Sepet Basariyla Silindi.");
        }
    }
}
