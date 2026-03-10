using ECommerce.WebUI.Services;
using Frontend.DtosLayer.CategoryDto;
using Frontend.DtosLayer.ProductsDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ECommerce.WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        public ProductsController(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";             
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await client.GetAsync("https://localhost:7166/api/Product/GetAllProducts");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                return View(values);
            }

            return View();
        }

        //Create Product get
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7166/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                //listeleme işlemlerinde deserialize
                var values = JsonConvert.DeserializeObject<List<CategoryListDto>>(jsonData);

                ViewBag.categories = values;
            }
            return View();
        }

        //Create Product post
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            //api ye istek göndermem için bir HTTPClient nesnesi oluşturdum
            var client = _httpClientFactory.CreateClient();

            /* var token = await _tokenService.GetAccessToken(HttpContext);*/
            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";
            //api isteğine authorization header ekler
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //ekleme ve güncelleme işlemlerinde serialize
            var jsonData = JsonConvert.SerializeObject(dto);

            //json verisini http içeriğine çevirme
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7166/api/Product", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return View(dto);
        }

        //Delete product
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"https://localhost:7166/api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            return RedirectToAction("Index", "Products", new { area = "Admin" });
        }

        //Update product
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            //bizi tanıması için header da token gönderdik
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //header da token gönderdikten sonra verimizi serialize şeklinde ileteceğiz. çünkü ekleme ve güncellemede serialize
            var response = await client.GetAsync($"https://localhost:7166/api/Product/GetProductById/{id}");

            if (response.IsSuccessStatusCode) //405 hata kodu alıyorum buradan devam edeceğim
            {
                //Http'den gelen içeriği string olarak oku
                var jsonData = await response.Content.ReadAsStringAsync();

                //gelen json datayı deserialize et çünkü bu listeleme işlemidir
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);

                //kategori değiştirebilmek için kategorileri listeleme
                var categoryResponse = await client.GetAsync("https://localhost:7166/api/Categories");

                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

                    var categoryList = JsonConvert.DeserializeObject<List<CategoryListDto>>(categoryJson);

                    ViewBag.categories = categoryList;
                }
                else
                {
                    ViewBag.categories = new List<CategoryListDto>();
                }
                return View(values);
            }

            return View();

        }

        //Update Product Post
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto dto)
        {
            var client = _httpClientFactory.CreateClient();

            var token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IkQ4OEY3NkVDNDFCOEU1QTZGMEQ5RUNEQ0UxQTlBMjRFIiwidHlwIjoiYXQrand0In0.eyJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MjIyIiwibmJmIjoxNzczMDY5Mzk2LCJpYXQiOjE3NzMwNjkzOTYsImV4cCI6MTc3NTY2MTM5NiwiYXVkIjpbImJhc2tldF9taWNyb3NlcnZpY2UiLCJjYXJnb19taWNyb3NlcnZpY2UiLCJjYXRhbG9nX21pY3Jvc2VydmljZSIsImRpc2NvdW50X21pY3Jvc2VydmljZSIsIm9yZGVyX21pY3Jvc2VydmljZSJdLCJzY29wZSI6WyJiYXNrZXQuZnVsbCIsImNhcmdvLmZ1bGwiLCJjYXRhbG9nLmZ1bGwiLCJkaXNjb3VudC5mdWxsIiwiZW1haWwiLCJJZGVudGl0eVNlcnZlckFwaSIsIm9wZW5pZCIsIm9yZGVyLmZ1bGwiLCJwcm9maWxlIiwib2ZmbGluZV9hY2Nlc3MiXSwiYW1yIjpbInBhc3N3b3JkIl0sImNsaWVudF9pZCI6IkVDb21tZXJjZUFkbWluSWQiLCJzdWIiOiI3MzI2YjkyZi02MzgxLTRiOTAtOGRiZC1kMzhhZDM1YWY2MzgiLCJhdXRoX3RpbWUiOjE3NzMwNjkzOTUsImlkcCI6ImxvY2FsIiwianRpIjoiRUU2MzAzNDZENDJDNjE4MzYxRTY1RjE0QkNEMzM3NjkifQ.Lk0NTsJQL-ue--XdqwXF0SkaCmwEJpwkwWPNQ2q3w8GuIs8ylY9nJUl5YkjYVCKogvMAIXn6Y-m_Tk1lmaKprohSZv-SbTvEfMVsAvNzUmqDoeFbwemKMwCR_xC_nQMLDQdRE1vHsNn9OiUDvL29elHO7GYQGtSuNqB9ei9yPH40Mb2979TI_QtCyutNy1mqKuw5qP7ImTu0SUyNu8RSeqmMitCHYk3wftMQlHQcN_FTs277Ls6XnDBlM646dtv0bz2L9t4gns4qnPbsr-171loLUsSVF-FUpgk8H3nfbd9oM6yfBIQ767GDwNMtO9zUSGr7MnJr37aB1Pjol_k5lw";

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonData = JsonConvert.SerializeObject(dto);

            //string veriyi Http içerisine koymamız gerekiyor
            StringContent stringContent = new StringContent(jsonData,Encoding.UTF8,"application/json");

            var response = await client.PutAsync($"https://localhost:7166/api/Product/{dto.ProductId}",stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            // ❗ tekrar kategori yükle
            var categoryResponse = await client.GetAsync("https://localhost:7166/api/Categories");

            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categoryList = JsonConvert.DeserializeObject<List<CategoryListDto>>(categoryJson);

                ViewBag.categories = categoryList;
            }

            return View(dto);
        }

    }
}
