using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Models;
using IMGCloud.Utilities.PasswordHashExtension;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Domain.Repositories;

namespace IMGCloud.Application.Implement.Users
{
    public class UserService : IUserService
    {
        private readonly IStringLocalizer<UserService> _stringLocalizer;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger,
            IStringLocalizer<UserService> stringLocalizer,
            IUserRepository userRepository)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _userRepository = userRepository;
        }
        public async Task<ResponeVM> CreateUserAsync(CreateUserVM model)
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
            var activeUser = await _userRepository.IsActiveUserAsync(model.UserName);
            try
            {
                if (activeUser)
                {
                    var isValidPassword = model.Password.VerifyPassword(model.Password);
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
    }
}
