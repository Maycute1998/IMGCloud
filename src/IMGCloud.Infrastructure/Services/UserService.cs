using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace IMGCloud.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserDetailRepository _userDetailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<UserService> _stringLocalizer;

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
            IUserDetailRepository userDetailRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _userDetailRepository = userDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
        }

        private async Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        {
            if (await _userRepository.IsExitsUserNameAsync(model.UserName!, cancellationToken)
                || await _userRepository.IsExitsUserEmailAsync(model.Email, cancellationToken))
            {
                throw new ArgumentException("User is existed");
            }

            await _userRepository.CreateUserAsync(model, cancellationToken);
        }

        private Task<UserDetail?> GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this._userDetailRepository.GetByUserNameAsync(userName, cancellationToken);

        public Task CreateUserDetailAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
        => _userRepository.CreateUserDetailAsync(userInfo, cancellationToken);

        public async Task<bool> IsActiveUserAsync(SignInContext model, CancellationToken cancellationToken)
        {

            var activeUser = await _userRepository.GetUserByUserNameAsync(model.UserName!);
            if (activeUser is not null)
            {
                var isValidPassword = model.Password.VerifyPassword(activeUser.Password);
                if (isValidPassword)
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

        public async Task<bool> IsExistEmailAsync(string email)
        {
            try
            {
                var user = await _userRepository.IsExitsUserEmailAsync(email);
                if (user)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method [{nameof(IsExistEmailAsync)}] {Environment.NewLine} Error: {ex.Message}");
            }
            return false;
        }

        private Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken)
        => this.IsExistEmailAsync(email, cancellationToken);

        private Task<int> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken)
        => _userRepository.GetUserIdByUserNameAsync(userName, cancellationToken);


        private Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
        => _userTokenRepository.GetExistedTokenAsync(userId, cancellationToken);

        private Task StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
        => _userTokenRepository.StoreTokenAsync(context, cancellationToken);


        private async Task RemoveTokenAsync(CancellationToken cancellationToken)
        {
            var curentUser = await _userRepository.GetUserByUserNameAsync(this.GetCurrentUserName!, cancellationToken);
            if (curentUser is null)
            {
                return;
            }

            await _userTokenRepository.RemoveTokenAsync(curentUser.Id, cancellationToken);
        }

        public Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        => _userDetailRepository.GetByUserIdAsync(id, cancellationToken);


        Task<int> IUserService.GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserIdByUserNameAsync(userName, cancellationToken);

        Task<bool> IUserService.IsActiveUserAsync(SignInContext user, CancellationToken cancellationToken)
        => this.IsActiveUserAsync(user, cancellationToken);



        Task<string?> IUserService.GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
        => this.GetExistedTokenAsync(userId, cancellationToken);

        Task IUserService.StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
        => this.StoreTokenAsync(context, cancellationToken);

        Task IUserService.RemoveTokenAsync(CancellationToken cancellationToken)
        => this.RemoveTokenAsync(cancellationToken);

        Task<UserDetail?> IUserService.GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserDetailByUserNameAsync(userName, cancellationToken);



        Task IUserService.CreateUserDetailAsync(UserDetailsRequest userDetail, CancellationToken cancellationToken)
        => this.CreateUserDetailAsync(userDetail, cancellationToken);

        Task IUserService.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        => this.CreateUserAsync(model, cancellationToken);
    }
}

