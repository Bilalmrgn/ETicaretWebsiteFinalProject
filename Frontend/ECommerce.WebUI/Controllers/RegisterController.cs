using Frontend.DtosLayer.RegisterDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ECommerce.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        // Challenge metodu, Program.cs'deki "oidc" ayarlarını kullanarak 
        // kullanıcıyı otomatik olarak IdentityServer'ın Authorize endpoint'ine fırlatır.

        //register get metod sayfa gösterimi için
        [HttpGet]
        public IActionResult Index()
        {
            //authorization code grant type kullanarak register işlemi
            //1. authorization code kullandığımız için client tarafında kullanıcıyı identity ye yönlendirmemiz gerkeiyor
            var props = new AuthenticationProperties
            {
                RedirectUri = "/"
            };

            props.Items.Add("return_to", "register");

            return Challenge(props,"oidc");
        }


        
    }
}
