using ECommerce.WebUI.Services.StatisticServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IStatisticService _statisticService;

        public AdminHomeController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        public async Task<IActionResult> Index()
        {
            var statistics = await _statisticService.GetDashboardStatisticsAsync();
            return View(statistics);
        }
    }
}
