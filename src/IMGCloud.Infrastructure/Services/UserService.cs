using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace IMGCloud.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserDetailRepository _userInfoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<UserService> _stringLocalizer;
        private readonly string className = typeof(UserRepository).FullName ?? string.Empty;

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
            _stringLocalizer = stringLocalizer;
        }
        private async Task<ApiResult<User>> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        {
            var result = new ApiResult<User>();
            if (await _userRepository.IsExitsUserNameAsync(model.UserName!, cancellationToken)
                || await _userRepository.IsExitsUserEmailAsync(model.Email, cancellationToken))
            {
                string msg = string.Format(_stringLocalizer["emailAlreadyExits"].ToString(), model.UserName);
                _logger.LogError($"Method [{nameof(CreateUserAsync)}] {Environment.NewLine} Error: {msg}");
            }
            else
            {
                var user = await _userRepository.CreateUserAsync(model, cancellationToken);
                result.Context = user;
                result.IsSucceeded = true;
                result.Message = _stringLocalizer["userCreated"].ToString();
            }
            return result;
        }

        public async Task<ApiResult> CreateUserInfoAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
        {
            var result = new ApiResult();
            try
            {
                await _userRepository.CreateUserInfoAsync(userInfo, cancellationToken);
                string msg = _stringLocalizer["userCreated"].ToString();
                result.Message = msg;
                result.IsSucceeded = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.IsSucceeded = false;
                _logger.LogError($"Method [{nameof(CreateUserAsync)}] {ex.Message}");
            }
            return result;
        }

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

        public Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        => _userInfoRepository.GetUserByIdAsync(id, cancellationToken);


        Task<int> IUserService.GetUserIdByUserNameAsync (string userName, CancellationToken cancellationToken)
        => this.GetUserIdByUserNameAsync(userName, cancellationToken);

        Task<bool> IUserService.IsActiveUserAsync (SignInContext user, CancellationToken cancellationToken)
        => this.IsActiveUserAsync(user, cancellationToken);



        Task<string?> IUserService.GetExistedTokenAsync (int userId, CancellationToken cancellationToken)
        => this.GetExistedTokenAsync(userId, cancellationToken);

        Task IUserService.StoreTokenAsync (UserTokenContext context, CancellationToken cancellationToken)
        => this.StoreTokenAsync(context, cancellationToken);

        Task IUserService.RemoveTokenAsync (CancellationToken cancellationToken)
        => this.RemoveTokenAsync(cancellationToken);

        Task<UserDetail?> IUserService.GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserDetailByUserNameAsync(userName, cancellationToken);

        Task<ApiResult> IUserService.CreateUserInfoAsync (UserDetailsRequest userInfo, CancellationToken cancellationToken)
        => this.CreateUserInfoAsync(userInfo, cancellationToken);

        Task<ApiResult<User>> IUserService.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        => this.CreateUserAsync(model, cancellationToken);
    }
}

