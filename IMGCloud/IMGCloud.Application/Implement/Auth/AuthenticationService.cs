using IMGCloud.Application.Interfaces.Auth;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Models;
using Microsoft.Extensions.Logging;

namespace IMGCloud.Application.Implement.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(ILogger<AuthenticationService> logger
            , IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public Task<TokenResult> RefreshTokenAsync(string refreshToken, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResult> SignInAsync(SigInVM model)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponeVM> SignUpAsync(CreateUserVM model)
        {
            return await _userService.CreateUserAsync(model);
        }
    }
}
