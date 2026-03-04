using Frontend.DtosLayer.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //GetAllCategory
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var response= await client.GetAsync("https://localhost:7166/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}






