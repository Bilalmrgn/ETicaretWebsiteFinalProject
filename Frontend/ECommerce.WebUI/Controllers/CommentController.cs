using Frontend.DtosLayer.CategoryDto;
using Frontend.DtosLayer.CommentDto;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CommentController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //Create comment
        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto dto)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Giriş yapılmamışsa Login sayfasına yönlendir. 
                // ReturnUrl olarak gelinen sayfayı (Ürün Detay) verebilirsin.
                return RedirectToAction("Index", "Login");
            }

            var client = _httpClientFactory.CreateClient("CommentClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/comments", stringContent);

            if (response.IsSuccessStatusCode)
            {
                TempData["CommentSuccess"] = "Yorumunuz başarıyla kaydedildi.";
                return RedirectToAction("Details", "Product", new { id = dto.ProductId });
            }
            return View();
        }

        //Update Comment
        [HttpPost]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto dto)
        {
            var client = _httpClientFactory.CreateClient("CommentClient");

            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Comments/{dto.UserCommentId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Product");
            }

            return View();
        }
    }
}
