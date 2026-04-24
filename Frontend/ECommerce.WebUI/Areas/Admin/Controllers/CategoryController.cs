using ECommerce.WebUI.Services;
using Frontend.DtosLayer.CategoryDto;
using Frontend.DtosLayer.ProductsDto;
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
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(IHttpClientFactory httpClientFactory, ITokenService tokenService, IWebHostEnvironment webHostEnvironment)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
            _webHostEnvironment = webHostEnvironment;
        }

        //GetAllCategory
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync("/catalog/category");

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
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var resourcePath = _webHostEnvironment.WebRootPath;
                var extension = Path.GetExtension(imageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var savePath = Path.Combine(resourcePath, "images", "categories", imageName);

                // Create directory if it doesn't exist
                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                dto.ImageUrl = "/images/categories/" + imageName;
            }

            var client = _httpClientFactory.CreateClient("CatalogClient");

            //ekleme ve güncelleme işlemlerinde serialize
            var jsonData = JsonConvert.SerializeObject(dto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("/catalog/category", stringContent);

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
            var client = _httpClientFactory.CreateClient("CatalogClient");

         
            var response = await client.DeleteAsync($"/catalog/category/{id}");

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
            var client = _httpClientFactory.CreateClient("CatalogClient");

       

            var response = await client.GetAsync($"/catalog/category/{id}");

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
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var resourcePath = _webHostEnvironment.WebRootPath;
                var extension = Path.GetExtension(imageFile.FileName);
                var imageName = Guid.NewGuid() + extension;
                var savePath = Path.Combine(resourcePath, "images", "categories", imageName);

                var directory = Path.GetDirectoryName(savePath);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                dto.ImageUrl = "/images/categories/" + imageName;
            }

            var client = _httpClientFactory.CreateClient("CatalogClient");

            //yazdığım güncellemeleri json olarak göndermem gerekiyor
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/catalog/category/{dto.CategoryId}", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryId(string id)
        {
            var client = _httpClientFactory.CreateClient("CatalogClient");

            var response = await client.GetAsync($"/catalog/product/by-category/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();

                var values = JsonConvert.DeserializeObject<List<ProductListDto>>(jsonData);

                return View(values);
            }

            return View(new List<ProductListDto>());
        }

    }
}






