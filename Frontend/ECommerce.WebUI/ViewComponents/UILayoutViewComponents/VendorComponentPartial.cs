using Frontend.DtosLayer.BrandDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class VendorComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public VendorComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

            var response = await client.GetAsync("https://localhost:7166/api/Brand");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

                return View(values);
            }

            return View(new List<ResultBrandDto>());

        }
    }
}
