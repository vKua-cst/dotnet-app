using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Services;
using Microsoft.AspNetCore.Authorization;

namespace App.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Connect()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Connect(ConnectViewModel model, string returnUrl = null)
        {
            bool result = await _authService.AuthenticateAsync(model);

            if (result)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else { return Redirect("/home/index"); }
            }
            
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Disconnect()
        {
            await _authService.LogOutAsync();

            return Redirect("/");
        }
    }
}
