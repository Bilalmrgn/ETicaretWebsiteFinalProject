using Frontend.DtosLayer.ContactDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        //create contact message
        [HttpPost]
        public async Task<IActionResult> CreateContactMessage(CreateContactDto dto)
        {
            var client = _httpClientFactory.CreateClient("ContactClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("api/Contact",stringContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["ContactSuccess"] = "Mesajınız başarıyla gönderildi. Teşekkür ederiz!";

                return RedirectToAction("Index", "Contact");
            }

            return View();
        }

    }
}
