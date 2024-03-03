using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Models;
using IMGCloud.Utilities.PasswordHashExtension;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace IMGCloud.Application.Implement.Users
{
    public class UserService : IUserService
    {
        private readonly IStringLocalizer<UserService> _stringLocalizer;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ILogger<UserService> logger,
            IStringLocalizer<UserService> stringLocalizer,
            IUserRepository userRepository,
            IUserTokenRepository userTokenRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ResponeVM> CreateUserAsync(UserVM model)
        {
            var res = new ResponeVM();
            try
            {
                if (await _userRepository.IsExitsUserNameAsync(model.UserName))
                {
                    res.Message = string.Format(_stringLocalizer["userNameAlreadyExits"].ToString(), model.UserName);
                }
                else if (await _userRepository.IsExitsUserEmailAsync(model.Email))
                {
                    res.Message = string.Format(_stringLocalizer["emailAlreadyExits"].ToString(), model.UserName);
                }
                else
                {
                    await _userRepository.CreateUserAsync(model);
                    res.Message = _stringLocalizer["userCreated"].ToString();
                    res.Status = true;
                        
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                _logger.LogError($"Method [{nameof(CreateUserAsync)}] {Environment.NewLine} Error: {ex.Message}");
            }
            return res;
        }

        public async Task<ResponeVM> IsActiveUserAsync(SigInVM model)
        {
            var result = new ResponeVM();
            var activeUser = await _userRepository.GetUserbyUserName(model.UserName);
            try
            {
                if (activeUser is not null)
                {
                    var isValidPassword = model.Password.VerifyPassword(activeUser.Password);
                    if (isValidPassword)
                    {
                        result.Status = true;
                        result.Message = _stringLocalizer["LoginSuccessfully"].ToString();
                        _logger.LogInformation($"Method {nameof(IsActiveUserAsync)} {result.Message}", result.Message);
                    }
                    else
                    {
                        result.Message = _stringLocalizer["LoginFailed"];
                        _logger.LogInformation($"Method {nameof(IsActiveUserAsync)} {Environment.NewLine} Error: {result.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError($"Method [{nameof(IsActiveUserAsync)}] {Environment.NewLine} Error: {ex.Message}");
            }
            return result;
        }

        public int GetUserId(string userName)
        {
            return _userRepository.GetUserId(userName);
        }

        public string GetExistedTokenFromDatabase(int userId)
        {
            return _userTokenRepository.GetExistedUserTokenFromDB(userId);
        }

        public ResponeVM StoreTokenAsync(TokenVM tokenModel)
        {
            return _userTokenRepository.StoreToken(tokenModel);
        }

        public string GetCurrentUserName
        {
            get
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault().Subject.Name;
                }
                return "Please login";
            }
        }
        public ResponeVM RemveToken()
        {
            var curentUserId = _userRepository.GetUserId(GetCurrentUserName);
            return _userTokenRepository.RemveToken(curentUserId);
        }
    }
}
