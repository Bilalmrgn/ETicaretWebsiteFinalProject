using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class HelpCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
