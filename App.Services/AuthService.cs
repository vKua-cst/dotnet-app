using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpContext _httpContext;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor.HttpContext;
        }
        public async Task<bool> AuthenticateAsync(ConnectViewModel model)
        {
            if (model == null)
                return false;

            if(model.Login == "Tarik" && model.Password == "pass")
            {
                var claims = new List<Claim>()
                {
                    new Claim("Name", "Tarik"),
                    new Claim("Role", "Dev"),
                    new Claim("FullName", "Tarik Ouali")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await _httpContext.SignInAsync(
                    "cookie",
                    principal
                );

                return true;
            }

            return false;
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
