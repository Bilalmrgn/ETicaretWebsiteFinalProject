using Catolog.DTOs.FeatureSliderDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminFeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminFeatureSliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //get all feature slider
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzcyOTc0OTczLCJpYXQiOjE3NzI5NzQ5NzMsImV4cCI6MTc3Mjk4MDk5MywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzI5NzQ5NzMsImlkcCI6ImxvY2FsIiwianRpIjoiNTAzNDE5NEU4M0Y5MzcxQkZFNzA1MDM4M0IzOTY1ODgifQ.tc8B0nO5TIDTcFu0OMdZOKSB3ZBjHkkqhik-WtBwS-RxOllIhRw6Cz1lPhk6rcnwPae7YjcL7-bLNRxX8PcByMeBSld8YWHmKuKzVz4fx-S4nTP2NdPpX3HH-r0JoWIcYf_P_0XhdYWk1i6e9g0kaYSU1LgYIMgsnWXO2wDLkWrW5vQ1LZOfzMz1V7LuxauGBHXtZwjRYxvQ84mpfB697cQzGMJ8fdv6M1nFZt800mDA7qMfN1P5gapSIMOtdrMJo8M2K0LSQSuqmVoLtZONQjCECTcc18OfQpSQhmglYoqOOIvX3cJuLD1zvadB7cchEbPVJh9K1GLIpaenP4C-VA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7166/api/FeatureSlider");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);

                return View(values);
            }

            return View();

        }
        //create feature slider get method
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        //Create Feature slider post method
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzcyOTc0OTczLCJpYXQiOjE3NzI5NzQ5NzMsImV4cCI6MTc3Mjk4MDk5MywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzI5NzQ5NzMsImlkcCI6ImxvY2FsIiwianRpIjoiNTAzNDE5NEU4M0Y5MzcxQkZFNzA1MDM4M0IzOTY1ODgifQ.tc8B0nO5TIDTcFu0OMdZOKSB3ZBjHkkqhik-WtBwS-RxOllIhRw6Cz1lPhk6rcnwPae7YjcL7-bLNRxX8PcByMeBSld8YWHmKuKzVz4fx-S4nTP2NdPpX3HH-r0JoWIcYf_P_0XhdYWk1i6e9g0kaYSU1LgYIMgsnWXO2wDLkWrW5vQ1LZOfzMz1V7LuxauGBHXtZwjRYxvQ84mpfB697cQzGMJ8fdv6M1nFZt800mDA7qMfN1P5gapSIMOtdrMJo8M2K0LSQSuqmVoLtZONQjCECTcc18OfQpSQhmglYoqOOIvX3cJuLD1zvadB7cchEbPVJh9K1GLIpaenP4C-VA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PostAsync("https://localhost:7166/api/FeatureSlide",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminFeatureSlider", new { area = "Admin" });
            }

            return View();
        }

        //delete feature slider 
        [HttpPost]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzcyOTc0OTczLCJpYXQiOjE3NzI5NzQ5NzMsImV4cCI6MTc3Mjk4MDk5MywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzI5NzQ5NzMsImlkcCI6ImxvY2FsIiwianRpIjoiNTAzNDE5NEU4M0Y5MzcxQkZFNzA1MDM4M0IzOTY1ODgifQ.tc8B0nO5TIDTcFu0OMdZOKSB3ZBjHkkqhik-WtBwS-RxOllIhRw6Cz1lPhk6rcnwPae7YjcL7-bLNRxX8PcByMeBSld8YWHmKuKzVz4fx-S4nTP2NdPpX3HH-r0JoWIcYf_P_0XhdYWk1i6e9g0kaYSU1LgYIMgsnWXO2wDLkWrW5vQ1LZOfzMz1V7LuxauGBHXtZwjRYxvQ84mpfB697cQzGMJ8fdv6M1nFZt800mDA7qMfN1P5gapSIMOtdrMJo8M2K0LSQSuqmVoLtZONQjCECTcc18OfQpSQhmglYoqOOIvX3cJuLD1zvadB7cchEbPVJh9K1GLIpaenP4C-VA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7166/api/FeatureSlide/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
            }

            return View();

        }

        //update feature slider get method
        public IActionResult UpdateFeatureSlider()
        {
            return View();
        }

        //update feature slider
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzcyOTc0OTczLCJpYXQiOjE3NzI5NzQ5NzMsImV4cCI6MTc3Mjk4MDk5MywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzI5NzQ5NzMsImlkcCI6ImxvY2FsIiwianRpIjoiNTAzNDE5NEU4M0Y5MzcxQkZFNzA1MDM4M0IzOTY1ODgifQ.tc8B0nO5TIDTcFu0OMdZOKSB3ZBjHkkqhik-WtBwS-RxOllIhRw6Cz1lPhk6rcnwPae7YjcL7-bLNRxX8PcByMeBSld8YWHmKuKzVz4fx-S4nTP2NdPpX3HH-r0JoWIcYf_P_0XhdYWk1i6e9g0kaYSU1LgYIMgsnWXO2wDLkWrW5vQ1LZOfzMz1V7LuxauGBHXtZwjRYxvQ84mpfB697cQzGMJ8fdv6M1nFZt800mDA7qMfN1P5gapSIMOtdrMJo8M2K0LSQSuqmVoLtZONQjCECTcc18OfQpSQhmglYoqOOIvX3cJuLD1zvadB7cchEbPVJh9K1GLIpaenP4C-VA";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"https://localhost:7166/api/FeatureSlide/{dto.FeatureSliderId}",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });

            }

            return View();
        }

    }
}
