using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Frontend.DtosLayer.OrderDtos;

namespace ECommerce.WebUI.Areas.Admin.ViewComponents.AdminViewComponents
{
    public class AdminSidebarComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminSidebarComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Mesajları getir
            var client = _httpClientFactory.CreateClient("ContactClient");
            var response = await client.GetAsync("/contact");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<dynamic>>(jsonData);
                // Sadece okunmamış mesajları say (IsRead == false)
                ViewBag.ContactCount = messages?.FindAll(x => (bool)x.isRead == false).Count ?? 0;
            }
            else
            {
                ViewBag.ContactCount = 0;
            }

            // Yeni siparişleri getir (Status == Pending)
            var orderClient = _httpClientFactory.CreateClient("OrderClient");
            var orderResponse = await orderClient.GetAsync("api/Order");
            if (orderResponse.IsSuccessStatusCode)
            {
                var orderJsonData = await orderResponse.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<ResultOrderDto>>(orderJsonData);
                ViewBag.NewOrderCount = orders?.Count(x => x.Status == OrderStatus.Pending) ?? 0;
            }
            else
            {
                ViewBag.NewOrderCount = 0;
            }

            return View();
        }
    }
}
