using Frontend.DtosLayer.DashboardDtos;
using System.Text.Json;

namespace ECommerce.WebUI.Services.StatisticServices
{
    public class StatisticService : IStatisticService
    {
        private readonly HttpClient _orderClient;
        private readonly HttpClient _identityClient;

        public StatisticService(IHttpClientFactory httpClientFactory)
        {
            _orderClient = httpClientFactory.CreateClient("OrderClient");
            _identityClient = httpClientFactory.CreateClient("IdentityClient");
        }

        public async Task<DashboardStatisticDto> GetDashboardStatisticsAsync()
        {
            var dto = new DashboardStatisticDto();

            try
            {
                // Fetch Order statistics
                var orderResponse = await _orderClient.GetAsync("api/Order/dashboard-statistics");
                if (orderResponse.IsSuccessStatusCode)
                {
                    var orderData = await orderResponse.Content.ReadFromJsonAsync<DashboardStatisticDto>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (orderData != null)
                    {
                        dto.TotalOrderCount = orderData.TotalOrderCount;
                        dto.TotalIncome = orderData.TotalIncome;
                        dto.TopSellingProducts = orderData.TopSellingProducts ?? new List<TopSellingProductDto>();
                        dto.SalesByCities = orderData.SalesByCities ?? new List<SalesByCityDto>();
                    }
                }

                // Fetch Identity statistics
                var identityResponse = await _identityClient.GetAsync("auth/dashboard/statistics"); // Ocelot route
                if (identityResponse.IsSuccessStatusCode)
                {
                    var identityData = await identityResponse.Content.ReadFromJsonAsync<DashboardStatisticDto>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (identityData != null)
                    {
                        dto.TotalUserCount = identityData.TotalUserCount;
                    }
                }

                // Calculate Expense (Let's make it 35% of Income as a dummy value for now, as no expense table exists)
                dto.TotalExpense = dto.TotalIncome * 0.35m; 
            }
            catch (Exception ex)
            {
                // Optionally log error
                Console.WriteLine($"Error fetching statistics: {ex.Message}");
            }

            return dto;
        }
    }
}
