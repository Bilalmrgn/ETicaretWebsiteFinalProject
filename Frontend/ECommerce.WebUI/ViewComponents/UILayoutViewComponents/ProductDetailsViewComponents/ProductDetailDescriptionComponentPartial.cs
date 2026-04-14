using Frontend.DtosLayer.ProductDetailDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents.ProductDetailsViewComponents
{
    public class ProductDetailDescriptionComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductDetailDescriptionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync($"/catalog/ProductDetail/GetByProductId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<GetByIdProductDetailDTOs>(jsonData);
                
                return View(value);
            }

            return View(new GetByIdProductDetailDTOs());
        }
    }
}
