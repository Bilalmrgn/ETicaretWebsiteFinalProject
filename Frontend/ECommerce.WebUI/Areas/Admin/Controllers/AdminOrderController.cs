using Frontend.DtosLayer.OrderDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    public class AdminOrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminOrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("OrderClient");

            var response = await client.GetAsync("api/Order");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultOrderDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultOrderDto>());
        }
    }
}
