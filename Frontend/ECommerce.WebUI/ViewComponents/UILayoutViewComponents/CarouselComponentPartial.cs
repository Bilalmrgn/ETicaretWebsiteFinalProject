using Frontend.DtosLayer.SliderFeatureDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class CarouselComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CarouselComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDEzNTgzLCJpYXQiOjE3NzMwMTM1ODMsImV4cCI6MTc3MzAxOTYwMywiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwMTM1ODMsImlkcCI6ImxvY2FsIiwianRpIjoiMkU3Q0MyMDJCNjg0RjA2OUUxQUQ5OTA4OTdDOTFFRTAifQ.yi5FsleSTmeGJadI6DcZz9NJDvkkgGWUaMh02ax3tc4uFyMvWcudCsJV4kIP6CI-96bGul2b5P4cvJSWpyqmE-aHgXTT0QcwwvlID8_MOhvdDcPvpef--y6TSVKE3woDqIZf6hB9L6QiCvg4Lq02ASagZ4dL9Ew6sT66cBmR0EzVDjst3wgh1dqN1AD7UWtVeTmV15cjCjAbIBFszqJia7oHfJzrDKd8QZOBVFqR5dMRnBZj72ZbUxE7axcjCiKf39QQNjs6ALCnGnjNfXM0DWZ-LgBQqM2xrxTm-rVOy_MIqG9g-Dog_E8p2vjOu8jgsswzWW1jWmb-MP0AHtaanQ";
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
    }
}
