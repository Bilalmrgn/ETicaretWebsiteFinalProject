using Basket.Dtos;
using Basket.Service.Interfaces;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Basket.Service.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DiscountService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultCouponDto?> GetByCodeAsync(string code)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }

            var response = await client.GetAsync($"https://localhost:7090/api/Discount/getbycode/{code}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ResultCouponDto>(jsonData);

                return result;
            }

            return null;
        }

        public async Task<string> ApplyDiscountAsync(string userId, string code)
        {
            var client = _httpClientFactory.CreateClient();
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }

            var response = await client.GetAsync($"https://localhost:7090/api/Discount/apply-discount?userId={userId}&code={code}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result.Trim('\"'); // Remove quotes from string response if any
            }

            return "Error";
        }
    }
}
