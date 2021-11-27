using System.Threading.Tasks;

namespace App.Services
{
    public interface IAuthService
    {
        Task<bool> AuthenticateAsync(ConnectViewModel model);
        Task LogOut();
    }
}
