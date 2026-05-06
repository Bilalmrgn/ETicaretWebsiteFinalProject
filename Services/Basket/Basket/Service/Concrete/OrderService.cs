using Basket.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;

namespace Basket.Service.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AnyCompletedOrderAsync(string userId)
        {
            var client = _httpClientFactory.CreateClient("OrderClient");

            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", token);
            }
            
            // Order API'den kullanıcının tüm siparişlerini çekiyoruz
            var response = await client.GetAsync("api/Order/GetMyOrders");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<OrderResponseDto>>(jsonData);

                // Status 2 = Completed
                return orders != null && orders.Any(x => x.Status == 2);
            }

            return false;
        }
    }

    // Basit bir DTO tanımı
    public class OrderResponseDto
    {
        public int Status { get; set; }
    }
}
