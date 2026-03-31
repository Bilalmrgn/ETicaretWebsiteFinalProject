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
            var client = _httpClientFactory.CreateClient("CatalogClient");


            var response = await client.GetAsync("api/FeatureSlider");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);

                return View(values);
            }


            return View(new List<ResultFeatureSliderDto>());
        }
    }
}
