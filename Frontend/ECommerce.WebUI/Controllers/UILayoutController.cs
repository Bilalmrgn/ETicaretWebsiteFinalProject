using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult _Layout()
        {
            return View();
        }
    }
}
