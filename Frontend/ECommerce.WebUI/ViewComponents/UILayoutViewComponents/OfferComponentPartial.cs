using Frontend.DtosLayer.SpecialOfferDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class OfferComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public OfferComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync("api/SpecialOffer");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);
                return View(values);
            }


            return View(new List<ResultSpecialOfferDto>());
        }
    }
}
