using ECommerce.WebUI.Services;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class ProductComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public ProductComponentPartial(IHttpClientFactory httpClientFactory,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = await _tokenService.GetAccessToken(HttpContext);

            var client = _httpClientFactory.CreateClient("CatalogClient" );

            var response = await client.GetAsync("/catalog/product/last10");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                return View(values);
            }
            return View(new List<ProductListDto>());  
        }
    }
}
