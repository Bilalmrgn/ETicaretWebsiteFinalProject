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
        private readonly IOrderService _orderService;
        public BasketService(IRedisService redisService, IDiscountService discountService, IOrderService orderService)
        {
            _discountService = discountService;
            _redisService = redisService;
            _orderService = orderService;
        }

        public async Task<string> ApplyDiscountAsync(string userId, string discountCode)
        {
            var basketJson = await _redisService.GetAsync(userId);

            if (string.IsNullOrEmpty(basketJson))
                return "BasketNotFound";

            var basket = JsonConvert.DeserializeObject<BasketTotalDto>(basketJson);

            if (basket == null)
                return "BasketNotFound";

            if (string.Equals(basket.DiscountCode, discountCode, StringComparison.OrdinalIgnoreCase))
            {
                return "AlreadyApplied";
            }

            var coupon = await _discountService.GetByCodeAsync(discountCode);

            if (coupon == null)
                return "Invalid";

            // Kupon sadece ilk alışverişe özel mi kontrolü
            if (coupon.IsFirstOrderOnly)
            {
                var hasCompletedOrder = await _orderService.AnyCompletedOrderAsync(userId);
                if (hasCompletedOrder)
                {
                    return "AlreadyApplied"; // Kullanıcı daha önce alışveriş yapmış
                }
            }

            basket.DiscountCode = coupon.Code;
            basket.DiscountRate = coupon.Rate;

            await _redisService.SetAsync(userId, JsonConvert.SerializeObject(basket));

            return "success";
        }

        public async Task DeleteAsync(string userId)
        {
            var existBasket = await _redisService.GetAsync(userId);

            if (existBasket == null)
            {
                throw new Exception("Sepet Bulunamadi. (Service/Concrete/BasketService)");
            }

            await _redisService.RemoveAsync(userId);
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

            // Kupon re-validation
            if (!string.IsNullOrEmpty(basket.DiscountCode))
            {
                var coupon = await _discountService.GetByCodeAsync(basket.DiscountCode);
                bool shouldRemoveCoupon = false;

                if (coupon == null)
                {
                    shouldRemoveCoupon = true; // Kupon artık mevcut değil
                }
                else if (coupon.IsFirstOrderOnly)
                {
                    var hasCompletedOrder = await _orderService.AnyCompletedOrderAsync(userId);
                    if (hasCompletedOrder)
                    {
                        shouldRemoveCoupon = true; // Kullanıcı artık ilk alışverişinde değil
                    }
                }

                if (shouldRemoveCoupon)
                {
                    basket.DiscountCode = null;
                    basket.DiscountRate = null;
                    await _redisService.SetAsync(userId, JsonConvert.SerializeObject(basket));
                }
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
