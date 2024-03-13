using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.AspNetCore.Http;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Domain.Entities;

namespace IMGCloud.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserDetailRepository _userInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string? GetCurrentUserName
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated == true)
                {
                    return _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault()?.Subject?.Name;
                }

                return default;
            }
        }

        public UserService(ILogger<UserService> logger,
            IStringLocalizer<UserService> stringLocalizer,
            IUserRepository userRepository,
            IUserTokenRepository userTokenRepository,
            IUserDetailRepository userInfoRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _userInfoRepository = userInfoRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        private async Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        {
            if (!(await _userRepository.IsExitsUserNameAsync(model.UserName!, cancellationToken))
                || !(await _userRepository.IsExitsUserEmailAsync(model.Email, cancellationToken)))
            {
                return;
            }

            await _userRepository.CreateUserAsync(model, cancellationToken);
        }

        public async Task<bool> IsActiveUserAsync(SignInContext model, CancellationToken cancellationToken)
        {

            var activeUser = await _userRepository.GetUserByUserNameAsync(model.UserName!, cancellationToken);
            if (activeUser is not null)
            {
                var isValidPassword = model.Password?.VerifyPassword(activeUser.Password);
                if (isValidPassword == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private Task<int> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken)
        => _userRepository.GetUserIdByUserNameAsync(userName, cancellationToken);


        private Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
        =>  _userTokenRepository.GetExistedTokenAsync(userId, cancellationToken);


        private Task StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
        =>  _userTokenRepository.StoreTokenAsync(context, cancellationToken);


        private async Task RemoveTokenAsync(CancellationToken cancellationToken)
        {
            var curentUser = await _userRepository.GetUserByUserNameAsync(GetCurrentUserName, cancellationToken);
            if (curentUser is null)
            {
                return;
            }

            await _userTokenRepository.RemoveTokenAsync(curentUser.Id, cancellationToken);
        }

        public Task<UserDetail?> GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => _userInfoRepository.GetUserDetailbyUserNameAsync(userName, cancellationToken);


        Task<int> IUserService.GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserIdByUserNameAsync(userName, cancellationToken);

        Task<bool> IUserService.IsActiveUserAsync(SignInContext user, CancellationToken cancellationToken)
        => this.IsActiveUserAsync(user, cancellationToken);

        Task IUserService.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        => this.CreateUserAsync(model, cancellationToken);

        Task<string?> IUserService.GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
        => this.GetExistedTokenAsync(userId, cancellationToken);

        Task IUserService.StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
        => this.StoreTokenAsync(context, cancellationToken);

        Task IUserService.RemoveTokenAsync(CancellationToken cancellationToken)
        => this.RemoveTokenAsync(cancellationToken);

        Task<UserDetail?> IUserService.GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserDetailByUserNameAsync(userName, cancellationToken);
    }
}
