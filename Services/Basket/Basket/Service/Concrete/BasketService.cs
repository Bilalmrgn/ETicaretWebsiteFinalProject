using Basket.Dtos;
using Basket.Service.Interfaces;
using System.Text.Json;

namespace Basket.Service.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IRedisService _redisService;
        public BasketService(IRedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task DeleteAsync(string userId)
        {
            var existBasket = await _redisService.GetAsync(userId);

            if (existBasket == null)
            {
                throw new Exception("Sepet Bulunamadi. (Service/Concrete/BasketService)");
            }

            await _redisService.RemoveAsync(existBasket);
        }

        public async Task<BasketTotalDto> GetBasketAsync(string userId)
        {
            var existBasket = await _redisService.GetAsync(userId);

            if (existBasket == null)
            {
                return new BasketTotalDto();
            }

            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }

        //update and create = save
        public async Task SaveAsync(BasketTotalDto basket)
        {
            await _redisService.SetAsync(basket.UserId, JsonSerializer.Serialize(basket));//benim basket kısmım = obje türünde. redis benden string ister bu yüzden serialize
        }
    }
}
