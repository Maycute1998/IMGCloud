using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Repositories;
using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Implement.Users
{
    public class UserService : IUserService
    {
        private readonly IStringLocalizer<UserService> _stringLocalizer;
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger,
            IStringLocalizer<UserService> stringLocalizer,
            IUserRepository _userRepository)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<ResponeVM> CreateUserAsync(CreateUserVM model)
        {
            var res = new ResponeVM();
            try
            {
                if(await _userRepository.IsExitsUserNameAsync(model.UserName))
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
                }
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                _logger.LogError($"Method [{nameof(CreateUserAsync)}] {Environment.NewLine} Error-{ex.Message}");
            }
            return res;
        }
    }
}
