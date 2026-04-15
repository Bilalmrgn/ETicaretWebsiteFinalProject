using ECommerce.WebUI.ViewModel;
using Frontend.DtosLayer.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _NavbarUILayoutComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Kategoriler
            var catalogClient = _httpClientFactory.CreateClient("CatalogClient");
            var categoryResponse = await catalogClient.GetAsync("/catalog/category");
            
            List<CategoryListDto> categories = new List<CategoryListDto>();
            if (categoryResponse.IsSuccessStatusCode)
            {
                var jsonData = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);
            }

            // 2. Favori Sayısını Çek (Oturum Kapalıysa 0 Gelecek)
            ViewBag.FavoriteCount = 0;
            if (User.Identity.IsAuthenticated)
            {
                var favoriteClient = _httpClientFactory.CreateClient("FavoriteClient");
                var favoriteResponse = await favoriteClient.GetAsync("https://localhost:7135/api/Favorite");
                if (favoriteResponse.IsSuccessStatusCode)
                {
                    var favJson = await favoriteResponse.Content.ReadAsStringAsync();
                    var favorites = JsonConvert.DeserializeObject<List<FavoriteModel>>(favJson);
                    ViewBag.FavoriteCount = favorites != null ? favorites.Count : 0;
                }
            }

            return View(categories);
        }
    }
}
