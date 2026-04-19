using Frontend.DtosLayer.UserListDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    //kullanıcıları admin sayfasında listelemek için yazılan bir controller dır
    [Area("admin")]
    public class UserListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("IdentityClient");

            var response = await client.GetAsync("/identity/user");

            //gelen response başarılı ise
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<UserListDto>>(jsonData);

                return View(values);
            }
            return View(new List<UserListDto>());
        }
    }
}
