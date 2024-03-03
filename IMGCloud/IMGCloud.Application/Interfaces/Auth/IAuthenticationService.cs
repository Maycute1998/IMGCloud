using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        Task<ResponeVM> SignUpAsync(UserVM model);
        Task<ResponeAuthVM> SignInAsync(SigInVM model);
        Task<ResponeVM> SignOutAsync();
    }
}
