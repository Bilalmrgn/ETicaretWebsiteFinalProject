using Frontend.DtosLayer.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class CategoriesComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoriesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync("api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);

                return View(values);
            }

            return View(new List<CategoryListDto>());
        }
    }
}
