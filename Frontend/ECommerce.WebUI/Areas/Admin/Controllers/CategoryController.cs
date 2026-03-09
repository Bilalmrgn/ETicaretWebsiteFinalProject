using ECommerce.WebUI.Services;
using Frontend.DtosLayer.CategoryDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public CategoryController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        //GetAllCategory
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            //token al çünkü api kısmımda authorize var
            /*            var token = await _tokenService.GetAccessToken(HttpContext);
            */
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);



            var response = await client.GetAsync("https://localhost:7166/api/Categories");


            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                //listeleme işlemlerinde deserialize
                var values = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);

                return View(values);
            }
            return View();
        }


        //Create Category
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            //HttpContext = Token'i cookie den alacağım anlamına geliyo
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //ekleme ve güncelleme işlemlerinde serialize
            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7166/api/Categories", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            return View(dto);
        }

        //Delete category
        [HttpPost]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();

            //cookie den access token al
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7166/api/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            return View();
        }

        //Update Category
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7166/api/Categories/{id}");

            if(response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                
                return View(values);
            }

            return View();
        }

        //Update Category
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            //token göndermek için
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //yazdığım güncellemeleri json olarak göndermem gerekiyor
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PutAsync($"https://localhost:7166/api/Categories/{dto.CategoryId}" , stringContent);

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });

            }

            return View();

        }


    }
}






