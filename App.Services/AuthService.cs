using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

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

            if(model.Login == "Chris" && model.Password == "pass")
            {
                var claims = new List<Claim>()
                {
                    new Claim("Name", "Chris"),
                    new Claim("Role", "Dev"),
                    new Claim("FullName", "Chris Creusat")
                };

                ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new(identity);

                await _httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal
                );

                return true;
            }

            return false;
        }

        public async Task LogOutAsync()
        {
            await _httpContext.SignOutAsync();
        }
    }
}
