using Frontend.DtosLayer.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto createOrderDto)
        {
            var client = _httpClientFactory.CreateClient("OrderClient");

            var jsonData = JsonConvert.SerializeObject(createOrderDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Order", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Success));
            }
            return View(createOrderDto);
        }

        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }
    }
}
