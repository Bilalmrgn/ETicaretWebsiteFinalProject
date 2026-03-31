using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public HomeController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
            }

            return View(vm);
        }
    }

    public class ErrorViewModel
    {
        public Duende.IdentityServer.Models.ErrorMessage Error { get; set; }
    }
}
