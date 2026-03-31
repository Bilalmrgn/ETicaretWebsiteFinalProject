using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //Kullanıcı giriş yapıp döndüğünde ilk durak burası olacağı için yönlendirme mantığını buraya kuruyoruz:
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return View();
                }
                
            }
            return View();
        }

        public IActionResult Index1()
        {
            return View();
        }
    }
}
