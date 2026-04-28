using Basket.Dtos;
using Basket.Service.Interfaces;
using Newtonsoft.Json;
using System.Text.Json;

namespace Basket.Service.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IRedisService _redisService;
        private readonly IDiscountService _discountService;
        public BasketService(IRedisService redisService,IDiscountService discountService)
        {
            _discountService = discountService;
            _redisService = redisService;
        }

        public async Task<bool> ApplyDiscountAsync(string userId, string discountCode)
        {
            var basketJson = await _redisService.GetAsync(userId);

            if (string.IsNullOrEmpty(basketJson))
                return false;

            var basket = JsonConvert.DeserializeObject<BasketTotalDto>(basketJson);


            var coupon = await _discountService.GetByCodeAsync(discountCode);

            if (coupon == null)
                return false;

            // 🔥 KU PONU REDIS’E YAZ
            basket.DiscountCode = coupon.Code;
            basket.DiscountRate = coupon.Rate;

            await _redisService.SetAsync(userId, JsonConvert.SerializeObject(basket));

            return true;
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
            var basketJson = await _redisService.GetAsync(userId);

            var basket = JsonConvert.DeserializeObject<BasketTotalDto>(basketJson);

            if (basket == null)
            {
                return new BasketTotalDto
                {
                    UserId = userId
                };
            }

            var total = basket.BasketItems.Sum(item => item.Price * item.Quantity);

            if (!string.IsNullOrEmpty(basket.DiscountCode) && basket.DiscountRate.HasValue)
            {
                var discountAmount = total * basket.DiscountRate.Value / 100;

                basket.TotalPrice = total - discountAmount;
            }
            else
            {
                basket.TotalPrice = total;
            }

            return basket;
        }

        //update and create = save
        public async Task SaveAsync(BasketTotalDto basket)
        {
            await _redisService.SetAsync(basket.UserId, System.Text.Json.JsonSerializer.Serialize(basket));//benim basket kısmım = obje türünde. redis benden string ister bu yüzden serialize
        }
    }
}
