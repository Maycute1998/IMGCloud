using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        Task<ResponeVM> SignUpAsync(CreateUserVM model);
        Task<ResponeAuthVM> SignInAsync(SigInVM model);
        Task<TokenResult> RefreshTokenAsync(string refreshToken, string accessToken = null);
        Task SignOutAsync();
    }
}
