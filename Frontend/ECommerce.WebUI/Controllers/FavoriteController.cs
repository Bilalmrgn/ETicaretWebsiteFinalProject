using ECommerce.WebUI.ViewModel;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.WebUI.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FavoriteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("FavoriteClient");
            var response = await client.GetAsync("https://localhost:7135/api/Favorite");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var favorites = JsonConvert.DeserializeObject<List<FavoriteModel>>(json);

                var productList = new List<ProductListDto>();
                var catalogClient = _httpClientFactory.CreateClient("CatalogClient");

                foreach (var item in favorites)
                {
                    var productResponse = await catalogClient.GetAsync($"/catalog/product/{item.ProductId}");
                    if (productResponse.IsSuccessStatusCode)
                    {
                        var productJson = await productResponse.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductListDto>(productJson);
                        if (product != null)
                        {
                            productList.Add(product);
                        }
                    }
                }

                return View(productList);
            }

            return View(new List<ProductListDto>());
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(string productId)
        {
            // kullanıcı giriş yaptı mı
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var client = _httpClientFactory.CreateClient("FavoriteClient");
            var jsonData = JsonConvert.SerializeObject(productId);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7135/api/Favorite", stringContent);

            if (response.IsSuccessStatusCode)
            {
                // Eğer referer (gelinen sayfa) varsa oraya dön, yoksa Index'e git
                var referer = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    return Redirect(referer);
                }
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(error);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(string productId)
        {
            // ✅ OTURUM KONTROLÜ
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;
                return Redirect(Request.Headers["Referer"].ToString());
            }

            var client = _httpClientFactory.CreateClient("FavoriteClient");

            var response = await client.DeleteAsync($"https://localhost:7135/api/Favorite/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var referer = Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    return Redirect(referer);
                }
                return RedirectToAction("Index");
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return BadRequest(error);
            }
        }
    }
}
