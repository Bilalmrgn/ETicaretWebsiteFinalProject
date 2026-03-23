using Frontend.DtosLayer.ContactDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //get all contact message
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczODQxODUxLCJpYXQiOjE3NzM4NDE4NTEsImV4cCI6MTc3NjQzMzg1MSwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiY29udGFjdF9taWNyb3NlcnZpY2UiLCJkaXNjb3VudF9taWNyb3NlcnZpY2UiLCJvcmRlcl9taWNyb3NlcnZpY2UiXSwic2NvcGUiOlsiYmFza2V0LmZ1bGwiLCJjYXJnby5mdWxsIiwiY2F0YWxvZy5mdWxsIiwiY29tbWVudC5mdWxsIiwiY29udGFjdC5mdWxsIiwiZGlzY291bnQuZnVsbCIsImVtYWlsIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvcGVuaWQiLCJvcmRlci5mdWxsIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwYXNzd29yZCJdLCJjbGllbnRfaWQiOiJFQ29tbWVyY2VBZG1pbklkIiwic3ViIjoiNzMyNmI5MmYtNjM4MS00YjkwLThkYmQtZDM4YWQzNWFmNjM4IiwiYXV0aF90aW1lIjoxNzczODQxODUxLCJpZHAiOiJsb2NhbCIsImp0aSI6IjBFNTcyQkU3NzY4OTJFN0MwRUM5MEU1REJFN0NBMDA0In0.k6v_40778uaNStZHgVHRnuzBgcRgYgkpy0q5kTkyBQ2t4Q-nIwNwIEWPnnhUR84en5F2vKd6Nsj4WfiUNBrCNKsKBHm_w4XAOD_FyJli43mXMY_3FinjRuHBL8GdXRoIY4gnbQ9uk2JhLPI24alVXpMqs0adROtO5pTI4EA5u9NA5HjORsapb5wS8rNj_IQKDj2T_DuzEQSkQzM5BSLglgb_LRlrC7VtcbdKMoGQW0vegylFmP6X3ED2JDRCWm7Fx0kybg28LNtfMZPJHdGlUhnXaihwmduZ4nIz5TF2krEgfDxw9z1HA9qeHfKZ6m3FAw2vJQkG6EzoszkD_ABuhA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7162/api/Contact");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultContactDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultContactDto>());
        }

        //get by id contact message
        public async Task<IActionResult> GetByIdContactMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczODQxODUxLCJpYXQiOjE3NzM4NDE4NTEsImV4cCI6MTc3NjQzMzg1MSwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiY29udGFjdF9taWNyb3NlcnZpY2UiLCJkaXNjb3VudF9taWNyb3NlcnZpY2UiLCJvcmRlcl9taWNyb3NlcnZpY2UiXSwic2NvcGUiOlsiYmFza2V0LmZ1bGwiLCJjYXJnby5mdWxsIiwiY2F0YWxvZy5mdWxsIiwiY29tbWVudC5mdWxsIiwiY29udGFjdC5mdWxsIiwiZGlzY291bnQuZnVsbCIsImVtYWlsIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvcGVuaWQiLCJvcmRlci5mdWxsIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwYXNzd29yZCJdLCJjbGllbnRfaWQiOiJFQ29tbWVyY2VBZG1pbklkIiwic3ViIjoiNzMyNmI5MmYtNjM4MS00YjkwLThkYmQtZDM4YWQzNWFmNjM4IiwiYXV0aF90aW1lIjoxNzczODQxODUxLCJpZHAiOiJsb2NhbCIsImp0aSI6IjBFNTcyQkU3NzY4OTJFN0MwRUM5MEU1REJFN0NBMDA0In0.k6v_40778uaNStZHgVHRnuzBgcRgYgkpy0q5kTkyBQ2t4Q-nIwNwIEWPnnhUR84en5F2vKd6Nsj4WfiUNBrCNKsKBHm_w4XAOD_FyJli43mXMY_3FinjRuHBL8GdXRoIY4gnbQ9uk2JhLPI24alVXpMqs0adROtO5pTI4EA5u9NA5HjORsapb5wS8rNj_IQKDj2T_DuzEQSkQzM5BSLglgb_LRlrC7VtcbdKMoGQW0vegylFmP6X3ED2JDRCWm7Fx0kybg28LNtfMZPJHdGlUhnXaihwmduZ4nIz5TF2krEgfDxw9z1HA9qeHfKZ6m3FAw2vJQkG6EzoszkD_ABuhA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7162/api/Contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<GetByIdContactDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //delete contanct message
        [HttpPost]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczODQxODUxLCJpYXQiOjE3NzM4NDE4NTEsImV4cCI6MTc3NjQzMzg1MSwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiY29udGFjdF9taWNyb3NlcnZpY2UiLCJkaXNjb3VudF9taWNyb3NlcnZpY2UiLCJvcmRlcl9taWNyb3NlcnZpY2UiXSwic2NvcGUiOlsiYmFza2V0LmZ1bGwiLCJjYXJnby5mdWxsIiwiY2F0YWxvZy5mdWxsIiwiY29tbWVudC5mdWxsIiwiY29udGFjdC5mdWxsIiwiZGlzY291bnQuZnVsbCIsImVtYWlsIiwiSWRlbnRpdHlTZXJ2ZXJBcGkiLCJvcGVuaWQiLCJvcmRlci5mdWxsIiwicHJvZmlsZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJwYXNzd29yZCJdLCJjbGllbnRfaWQiOiJFQ29tbWVyY2VBZG1pbklkIiwic3ViIjoiNzMyNmI5MmYtNjM4MS00YjkwLThkYmQtZDM4YWQzNWFmNjM4IiwiYXV0aF90aW1lIjoxNzczODQxODUxLCJpZHAiOiJsb2NhbCIsImp0aSI6IjBFNTcyQkU3NzY4OTJFN0MwRUM5MEU1REJFN0NBMDA0In0.k6v_40778uaNStZHgVHRnuzBgcRgYgkpy0q5kTkyBQ2t4Q-nIwNwIEWPnnhUR84en5F2vKd6Nsj4WfiUNBrCNKsKBHm_w4XAOD_FyJli43mXMY_3FinjRuHBL8GdXRoIY4gnbQ9uk2JhLPI24alVXpMqs0adROtO5pTI4EA5u9NA5HjORsapb5wS8rNj_IQKDj2T_DuzEQSkQzM5BSLglgb_LRlrC7VtcbdKMoGQW0vegylFmP6X3ED2JDRCWm7Fx0kybg28LNtfMZPJHdGlUhnXaihwmduZ4nIz5TF2krEgfDxw9z1HA9qeHfKZ6m3FAw2vJQkG6EzoszkD_ABuhA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7162/api/Contact/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminContact", new { Area = "Admin" });
            }

            return View();
        }
    }
}
