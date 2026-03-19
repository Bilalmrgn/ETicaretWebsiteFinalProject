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
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczODQxODUxLCJpYXQiOjE3NzM4NDE4NTEsImV4cCI6MTc3NjQzMzg1MSwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiY29udGFjdF9taWNyb3NlcnZpY2UiLCJkaXNjb3VudF9taWNyb3NlcnZpY2UiLCJvcmRlcl9taWNyb3NlcnZpY2UiXSwic2NvcGUiOlsiYmFza2V0LmZ1bGwiLCJjYXJnby5mdWxsIiwiY2F0YWxvZy5mdWxsIiwiY29tbWVudC5mdWxsIiwiY29udGFjdC5mdWxsIiwiZGlzY291bnQuZnVsbCIsImVtYWlsIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvcGVuaWQiLCJvcmRlci5mdWxsIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwYXNzd29yZCJdLCJjbGllbnRfaWQiOiJFQ29tbWVyY2VBZG1pbklkIiwic3ViIjoiNzMyNmI5MmYtNjM4MS00YjkwLThkYmQtZDM4YWQzNWFmNjM4IiwiYXV0aF90aW1lIjoxNzczODQxODUxLCJpZHAiOiJsb2NhbCIsImp0aSI6IjBFNTcyQkU3NzY4OTJFN0MwRUM5MEU1REJFN0NBMDA0In0.k6v_40778uaNStZHgVHRnuzBgcRgYgkpy0q5kTkyBQ2t4Q-nIwNwIEWPnnhUR84en5F2vKd6Nsj4WfiUNBrCNKsKBHm_w4XAOD_FyJli43mXMY_3FinjRuHBL8GdXRoIY4gnbQ9uk2JhLPI24alVXpMqs0adROtO5pTI4EA5u9NA5HjORsapb5wS8rNj_IQKDj2T_DuzEQSkQzM5BSLglgb_LRlrC7VtcbdKMoGQW0vegylFmP6X3ED2JDRCWm7Fx0kybg28LNtfMZPJHdGlUhnXaihwmduZ4nIz5TF2krEgfDxw9z1HA9qeHfKZ6m3FAw2vJQkG6EzoszkD_ABuhA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("https://localhost:7162/api/Contact",stringContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["ContactSuccess"] = "Mesajınız başarıyla gönderildi. Teşekkür ederiz!";

                return RedirectToAction("Index", "Contact");
            }

            return View();
        }

    }
}
