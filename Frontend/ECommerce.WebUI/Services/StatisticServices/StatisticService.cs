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
                var orderTask = _orderClient.GetAsync("api/Order/dashboard-statistics");
                var identityTask = _identityClient.GetAsync("auth/dashboard/statistics");

                await Task.WhenAll(orderTask, identityTask);

                var orderResponse = await orderTask;
                var identityResponse = await identityTask;

                if (orderResponse.IsSuccessStatusCode)
                {
                    var orderData = await orderResponse.Content.ReadFromJsonAsync<DashboardStatisticDto>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (orderData != null)
                    {
                        dto.TotalOrderCount = orderData.TotalOrderCount;
                        dto.TotalIncome = orderData.TotalIncome;
                        dto.TopSellingProducts = orderData.TopSellingProducts ?? new List<TopSellingProductDto>();
                        dto.SalesByCities = orderData.SalesByCities ?? new List<SalesByCityDto>();
                    }
                }

                if (identityResponse.IsSuccessStatusCode)
                {
                    var identityData = await identityResponse.Content.ReadFromJsonAsync<DashboardStatisticDto>(
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (identityData != null)
                    {
                        dto.TotalUserCount = identityData.TotalUserCount;
                    }
                }

                dto.TotalExpense = dto.TotalIncome * 0.35m;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching statistics: {ex.Message}");
            }

            return dto;
        }
    }
}
