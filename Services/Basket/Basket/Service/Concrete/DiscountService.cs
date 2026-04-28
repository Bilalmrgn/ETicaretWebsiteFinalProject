using Basket.Dtos;
using Basket.Service.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.Service.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscountService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResultCouponDto?> GetByCodeAsync(string code)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:7090/api/Discount/getbycode/{code}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResultCouponDto>(jsonData);

                return result;
            }

            return null;
        }
    }
}
