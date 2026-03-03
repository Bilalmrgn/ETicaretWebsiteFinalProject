using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        //AllProduct
        public IActionResult GetAllProduct()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
