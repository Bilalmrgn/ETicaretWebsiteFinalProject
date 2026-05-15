using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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

            return View();
        }
    }
}
