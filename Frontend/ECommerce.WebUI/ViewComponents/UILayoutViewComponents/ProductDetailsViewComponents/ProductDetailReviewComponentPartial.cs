using Frontend.DtosLayer.CommentDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ECommerce.WebUI.ViewComponents.UILayoutViewComponents.ProductDetailsViewComponents
{
    public class ProductDetailReviewComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient("CommentClient");

            var response = await client.GetAsync($"/comments/GetCommentListByProductId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var comments = JsonConvert.DeserializeObject<List<ResultCommentListDto>>(jsonData);

                return View(comments);
            }

            return View(new List<ResultCommentListDto>());
        }
    }
}
