using ECommerce.WebUI.Services;
using ECommerce.WebUI.ViewModel;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using ECommerce.WebUI.ViewModel;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents
{
    public class ProductComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public ProductComponentPartial(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var catalogClient = _httpClientFactory.CreateClient("CatalogClient");
            var response = await catalogClient.GetAsync("/catalog/product/last10");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                // Favorileri al
                var favoriteClient = _httpClientFactory.CreateClient("FavoriteClient");
                var favoriteResponse = await favoriteClient.GetAsync("https://localhost:7135/api/Favorite");

                List<string> favoriteProductIds = new List<string>();
                if (favoriteResponse.IsSuccessStatusCode)
                {
                    var favJson = await favoriteResponse.Content.ReadAsStringAsync();
                    var favorites = JsonConvert.DeserializeObject<List<FavoriteModel>>(favJson);
                    favoriteProductIds = favorites.Select(x => x.ProductId).ToList();
                }

                var model = new List<ProductWithFavoriteViewModel>();
                var commentClient = _httpClientFactory.CreateClient("CommentClient");

                foreach (var p in products)
                {
                    var ratingResponse = await commentClient.GetAsync($"/comments/GetProductRating/{p.ProductId}");
                    if (ratingResponse.IsSuccessStatusCode)
                    {
                        var ratingJson = await ratingResponse.Content.ReadAsStringAsync();
                        var ratingData = JsonConvert.DeserializeObject<dynamic>(ratingJson);
                        p.AverageRating = (double)ratingData.averageRating;
                        p.CommentCount = (int)ratingData.commentCount;
                    }

                    model.Add(new ProductWithFavoriteViewModel
                    {
                        Product = p,
                        IsFavorite = favoriteProductIds.Contains(p.ProductId)
                    });
                }

                return View(model);
            }

            return View(new List<ProductWithFavoriteViewModel>());
        }
    }
}
