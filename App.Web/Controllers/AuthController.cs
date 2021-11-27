using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Services;


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
        public IActionResult Disconnect()
        {
            return View();
        }
    }
}
