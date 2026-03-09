using Frontend.DtosLayer.SpecialOfferDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSpecialOfferController : Controller
    {
        
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminSpecialOfferController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //List all Special offer
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7166/api/SpecialOffer");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        //create special offer
        public IActionResult CreateSpecialOffer()
        {
            return View();
        }

        //create special offer Post method
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";

            //ilk önce sistemin beni tanıması gerekiyor
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7166/api/SpecialOffer",stringContent);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });
            }

            return View();
        }

        //update special offer get method
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7166/api/SpecialOffer/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<UpdateSpecialOfferDto>(jsonData);

                return View(values);
            }

            return View();
        }

        //update special offer post metod
        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PutAsync($"https://localhost:7166/api/SpecialOffer/{dto.SpecialOfferId}",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });
            }

            return View();
        }

        //delete special offer post method
        [HttpPost]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7166/api/SpecialOffer/{id}");

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminSpecialOffer", new { area = "Admin" });

            }

            return View();
        }
    }
}
