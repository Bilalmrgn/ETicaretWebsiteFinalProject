using Frontend.DtosLayer.BasketDtos;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasketController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //sepet sayfası 
        //get basket
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient("BasketClient");
            var catalogClient = _httpClientFactory.CreateClient("CatalogClient");

            //güncel basket i çekiyoruz
            var response = await client.GetAsync("https://localhost:7178/api/Basket");

            if (!response.IsSuccessStatusCode)
            {
                return View(new BasketTotalDto());
            }

            var basketJson = await response.Content.ReadAsStringAsync();
            var basket = JsonConvert.DeserializeObject<BasketTotalDto>(basketJson);

            if (basket != null && basket.BasketItems != null)
            {
                //sepetteki her ürün için catalog/product tan veri çek ve güncel verilerle sepeti besle
                foreach (var item in basket.BasketItems)
                {
                    var productResponse = await catalogClient.GetAsync($"/catalog/product/{item.ProductId}");

                    if (productResponse.IsSuccessStatusCode)
                    {
                        var productJson = await productResponse.Content.ReadAsStringAsync();
                        var product = JsonConvert.DeserializeObject<ProductListDto>(productJson);
                        if (product != null)
                        {
                            item.Price = product.ProductPrice;
                            item.ProductName = product.ProductName;
                            item.ProductImageUrl = product.ProductImageUrl;
                        }
                    }
                }
            }

            return View(basket);
        }

        //indirim kuponu uygulama işlemi //eve gelince discount api ve basket api yi düzenle basket api de discount api ye istek atacaksın
        [HttpPost]
        public async Task<IActionResult> ApplyDiscount(string discountCode)
        {
            var client = _httpClientFactory.CreateClient("BasketClient");

            var response = await client.PostAsync($"https://localhost:7178/api/Basket/apply-discount?discountCode={discountCode}", null);

            if (!response.IsSuccessStatusCode)
            {
                TempData["DiscountError"] = "Kupon kodu geçersiz veya uygulanamadı.";
            }
            else
            {
                TempData["DiscountSuccess"] = "Kupon başarıyla uygulandı!";
            }

            return RedirectToAction("Index");
        }


        //update basket and create basket
        public async Task<IActionResult> SaveBasket(BasketTotalDto basket)
        {
            var client = _httpClientFactory.CreateClient("BasketClient");

            var jsonData = JsonConvert.SerializeObject(basket);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7178/api/Basket", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Index");
        }

        //sepete ürün ekleme işlemleri
        public async Task<IActionResult> AddToBasket(string productId, int quantity = 1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;

                var referer = Request.Headers["Referer"].ToString();

                return !string.IsNullOrEmpty(referer) ? Redirect(referer) : RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient("BasketClient");

            //mevcut sepeti korumak ve üzerine ekleme yapmak için get basket yptık
            var response = await client.GetAsync("https://localhost:7178/api/Basket");

            BasketTotalDto basket;

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                basket = JsonConvert.DeserializeObject<BasketTotalDto>(jsonData);
            }
            else
            {
                basket = new BasketTotalDto();
            }

            var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;

                var updatedJson = JsonConvert.SerializeObject(basket);

                var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");

                await client.PostAsync("https://localhost:7178/api/Basket", updatedContent);

                return RedirectToAction("Index");
            }
            else
            {
                var catalogClient = _httpClientFactory.CreateClient("CatalogClient");

                var productResponse = await catalogClient.GetAsync($"/catalog/product/{productId}");

                if (!productResponse.IsSuccessStatusCode)
                {
                    return BadRequest("Ürün bilgisi alınamadı");
                }

                var productJson = await productResponse.Content.ReadAsStringAsync();

                var product = JsonConvert.DeserializeObject<ProductListDto>(productJson);

                if (product == null)
                {
                    return BadRequest("Ürün deserialize edilemedi");
                }
                basket.BasketItems.Add(new BasketItemDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Price = product.ProductPrice,
                    Quantity = quantity,
                    ProductImageUrl = product.ProductImageUrl
                });

                var content = new StringContent(JsonConvert.SerializeObject(basket), Encoding.UTF8, "application/json");

                await client.PostAsync("https://localhost:7178/api/Basket", content);

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient("BasketClient");
            var response = await client.GetAsync("https://localhost:7178/api/Basket");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var basket = JsonConvert.DeserializeObject<BasketTotalDto>(jsonData);

                var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
                if (existingItem != null)
                {
                    basket.BasketItems.Remove(existingItem);

                    var updatedJson = JsonConvert.SerializeObject(basket);
                    var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");
                    await client.PostAsync("https://localhost:7178/api/Basket", updatedContent);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DecreaseBasketItem(string productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["LoginRequired"] = true;
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient("BasketClient");
            var response = await client.GetAsync("https://localhost:7178/api/Basket");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var basket = JsonConvert.DeserializeObject<BasketTotalDto>(jsonData);

                var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
                if (existingItem != null)
                {
                    if (existingItem.Quantity > 1)
                    {
                        existingItem.Quantity--;
                    }
                    else
                    {
                        // 1'in altına düşüyorsa sepetten komple çıkar
                        basket.BasketItems.Remove(existingItem);
                    }

                    var updatedJson = JsonConvert.SerializeObject(basket);
                    var updatedContent = new StringContent(updatedJson, Encoding.UTF8, "application/json");
                    await client.PostAsync("https://localhost:7178/api/Basket", updatedContent);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
