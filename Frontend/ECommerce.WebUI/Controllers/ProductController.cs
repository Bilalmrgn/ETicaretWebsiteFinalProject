using ECommerce.WebUI.Services;
using ECommerce.WebUI.ViewModel;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public ProductController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        public IActionResult GetAllProduct()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");
            var response = await client.GetAsync($"/catalog/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetProductByIdDto>(jsonData);
                return View(values);
            }
            return View();
        }

        // kategoriye göre ürünleri listeleme
        public async Task<IActionResult> GetProductsByCategoryId(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");
            var response = await client.GetAsync($"/catalog/product/by-category/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                // favorileri alıyoruz
                var favoriteClient = _httpClientFactory.CreateClient("FavoriteClient");
                var favoriteResponse = await favoriteClient.GetAsync("https://localhost:7135/api/Favorite");

                List<string> favoriteProductIds = new List<string>();
                if (favoriteResponse.IsSuccessStatusCode)
                {
                    var favJson = await favoriteResponse.Content.ReadAsStringAsync();
                    var favorites = JsonConvert.DeserializeObject<List<FavoriteModel>>(favJson);
                    favoriteProductIds = favorites.Select(f => f.ProductId).ToList();
                }

                var model = products.Select(p => new ProductWithFavoriteViewModel
                {
                    Product = p,
                    IsFavorite = favoriteProductIds.Contains(p.ProductId)
                }).ToList();

                return View(model);
            }
            return View(new List<ProductWithFavoriteViewModel>());
        }
    }
}
