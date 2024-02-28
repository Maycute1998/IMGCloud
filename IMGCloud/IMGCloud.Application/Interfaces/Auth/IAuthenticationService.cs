using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Auth
{
    public interface IAuthenticationService
    {
        Task<ResponeVM> SignUpAsync(SignUpVM model);
        Task<TokenResult> SignInAsync(SigInVM model);
        Task<TokenResult> RefreshTokenAsync(string refreshToken, string accessToken = null);
        Task SignOutAsync();
    }
}
