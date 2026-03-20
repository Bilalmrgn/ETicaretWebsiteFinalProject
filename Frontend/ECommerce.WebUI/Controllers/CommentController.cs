using Frontend.DtosLayer.CategoryDto;
using Frontend.DtosLayer.CommentDto;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //Create comment
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczNjY1ODAzLCJpYXQiOjE3NzM2NjU4MDMsImV4cCI6MTc3NjI1NzgwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiZGlzY291bnRfbWljcm9zZXJ2aWNlIiwib3JkZXJfbWljcm9zZXJ2aWNlIl0sInNjb3BlIjpbImJhc2tldC5mdWxsIiwiY2FyZ28uZnVsbCIsImNhdGFsb2cuZnVsbCIsImNvbW1lbnQuZnVsbCIsImRpc2NvdW50LmZ1bGwiLCJlbWFpbCIsIklkZW50aXR5U2VydmVyQXBpIiwib3BlbmlkIiwib3JkZXIuZnVsbCIsInByb2ZpbGUiLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicGFzc3dvcmQiXSwiY2xpZW50X2lkIjoiRUNvbW1lcmNlQWRtaW5JZCIsInN1YiI6IjczMjZiOTJmLTYzODEtNGI5MC04ZGJkLWQzOGFkMzVhZjYzOCIsImF1dGhfdGltZSI6MTc3MzY2NTgwMywiaWRwIjoibG9jYWwiLCJqdGkiOiJFQTIxRUEyRjY4RUY4RUQwRDU5QjI4MzI3NzlEQzY0RSJ9.w0ydHh7nReZtscvO5rTmHWejxdnjBMInJfP7b5PwvlB5jUEYDKm2qVQIxvCRcEMDVaVhOCftEQ8FKZ-nUTIdKuMfW_D_3BO7bDr1KLW8zcoEFcsUbHDMXLH6SWX99-wFdLd0d6ID3_OXxHPwnW7SUT8iT4R3PKFx1xFdZz1mWUZ0f6CKTdvCXHrf0csMDhz6qzw6PFK8UR_PAcmWZIai36O9VRKC05tQXPbbwexcCVnQPxNmEaH4WJJZfn5Um6c7dUhiNrZdWidB7v3VF-iKTpU3B5JimE-X1P2Q2MGJOdLFgCn9E_ECEK5xuT_atKHY7Lm6glNAnkFRxP9q3IH4Bw";
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7221/api/Comments", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Product", new { id = dto.ProductId });
            }
            return View();
        }

        //Update Comment
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczNjY1ODAzLCJpYXQiOjE3NzM2NjU4MDMsImV4cCI6MTc3NjI1NzgwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImNvbW1lbnRfbWljcm9zZXJ2aWNlIiwiZGlzY291bnRfbWljcm9zZXJ2aWNlIiwib3JkZXJfbWljcm9zZXJ2aWNlIl0sInNjb3BlIjpbImJhc2tldC5mdWxsIiwiY2FyZ28uZnVsbCIsImNhdGFsb2cuZnVsbCIsImNvbW1lbnQuZnVsbCIsImRpc2NvdW50LmZ1bGwiLCJlbWFpbCIsIklkZW50aXR5U2VydmVyQXBpIiwib3BlbmlkIiwib3JkZXIuZnVsbCIsInByb2ZpbGUiLCJvZmZsaW5lX2FjY2VzcyJdLCJhbXIiOlsicGFzc3dvcmQiXSwiY2xpZW50X2lkIjoiRUNvbW1lcmNlQWRtaW5JZCIsInN1YiI6IjczMjZiOTJmLTYzODEtNGI5MC04ZGJkLWQzOGFkMzVhZjYzOCIsImF1dGhfdGltZSI6MTc3MzY2NTgwMywiaWRwIjoibG9jYWwiLCJqdGkiOiJFQTIxRUEyRjY4RUY4RUQwRDU5QjI4MzI3NzlEQzY0RSJ9.w0ydHh7nReZtscvO5rTmHWejxdnjBMInJfP7b5PwvlB5jUEYDKm2qVQIxvCRcEMDVaVhOCftEQ8FKZ-nUTIdKuMfW_D_3BO7bDr1KLW8zcoEFcsUbHDMXLH6SWX99-wFdLd0d6ID3_OXxHPwnW7SUT8iT4R3PKFx1xFdZz1mWUZ0f6CKTdvCXHrf0csMDhz6qzw6PFK8UR_PAcmWZIai36O9VRKC05tQXPbbwexcCVnQPxNmEaH4WJJZfn5Um6c7dUhiNrZdWidB7v3VF-iKTpU3B5JimE-X1P2Q2MGJOdLFgCn9E_ECEK5xuT_atKHY7Lm6glNAnkFRxP9q3IH4Bw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7221/api/Comments/{dto.UserCommentId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Product");
            }

            return View();
        }
    }
}
