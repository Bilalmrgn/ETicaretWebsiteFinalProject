using Frontend.DtosLayer.DashboardDtos;

namespace ECommerce.WebUI.Services.StatisticServices
{
    public interface IStatisticService
    {
        Task<DashboardStatisticDto> GetDashboardStatisticsAsync();
    }
}
