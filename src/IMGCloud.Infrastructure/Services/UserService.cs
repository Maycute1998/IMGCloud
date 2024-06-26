﻿using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

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
        private readonly IAmazonBucketService _amazonBucketService;
        private readonly IGoogleCloudService _googleCloudService;
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
            IHttpContextAccessor httpContextAccessor,
            IAmazonBucketService amazonBucketService,
            IGoogleCloudService googleCloudService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userTokenRepository = userTokenRepository;
            _userDetailRepository = userDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _stringLocalizer = stringLocalizer;
            _amazonBucketService = amazonBucketService;
            _googleCloudService = googleCloudService;
        }

        private async Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        {
            try
            {
                if (await _userRepository.IsExitsUserNameAsync(model.UserName!, cancellationToken)
                || await _userRepository.IsExitsUserEmailAsync(model.Email, cancellationToken))
                {
                    _logger.LogError(_stringLocalizer["RegisterFailed"]);
                }

                await _userRepository.CreateUserAsync(model, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private Task<UserDetailContext?> GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => _userDetailRepository.GetByUserNameAsync(userName, cancellationToken);

        private async Task CreateUserDetailAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
        {

            var photoUrl = await _googleCloudService.UploadFileAsync(userInfo.Photo, cancellationToken);
            userInfo.Photo = photoUrl;

            await _userRepository.CreateUserDetailAsync(userInfo, cancellationToken);
        }

        public async Task<bool> IsActiveUserAsync(SignInContext model, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetUserByUserNameAsync(model.UserName!);
            if(entity is not null && entity.Status == Status.Active)
            {
               var isCorrectPassword = model.Password.VerifyPassword(entity.Password);
                if (!isCorrectPassword)
                {
                    _logger.LogError(_stringLocalizer["incorrectUserNameOrPassword"]);
                    return false;
                }
                return true;
            }    
            return false;            
        }

        private Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken)
        => _userRepository.IsExitsUserEmailAsync(email, cancellationToken);

        private Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        => _userRepository.GetUserByUserNameAsync(userName, cancellationToken);

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

        private Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        => _userDetailRepository.GetByUserIdAsync(id, cancellationToken);

        private Task<string> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
        => _userRepository.ForgotPasswordAsync(email, cancellationToken);

        private Task ResetPasswordAsync(ResetPasswordRequest context, CancellationToken cancellationToken = default)
        =>_userRepository.ResetPasswordAsync(context, cancellationToken);

        #region Implementation of IUserService
        Task<bool> IUserService.IsExistEmailAsync(string email, CancellationToken cancellationToken)
        => this.IsExistEmailAsync(email, cancellationToken);
        Task<UserDetail?> IUserService.GetUserByIdAsync(int id, CancellationToken cancellationToken)
        => this.GetUserByIdAsync(id, cancellationToken);
        Task<User> IUserService.GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserByUserNameAsync(userName, cancellationToken);
        Task<bool> IUserService.IsActiveUserAsync(SignInContext user, CancellationToken cancellationToken)
        => this.IsActiveUserAsync(user, cancellationToken);
        Task<string?> IUserService.GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
        => this.GetExistedTokenAsync(userId, cancellationToken);
        Task IUserService.StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
        => this.StoreTokenAsync(context, cancellationToken);
        Task IUserService.RemoveTokenAsync(CancellationToken cancellationToken)
        => this.RemoveTokenAsync(cancellationToken);
        Task IUserService.CreateUserDetailAsync(UserDetailsRequest userDetail, CancellationToken cancellationToken)
        => this.CreateUserDetailAsync(userDetail, cancellationToken);
        Task IUserService.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
        => this.CreateUserAsync(model, cancellationToken);
        Task<UserDetailContext?> IUserService.GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken)
        => this.GetUserDetailByUserNameAsync(userName, cancellationToken);
        Task<string> IUserService.ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
        => this.ForgotPasswordAsync(email, cancellationToken);
        Task IUserService.ResetPasswordAsync(ResetPasswordRequest context, CancellationToken cancellationToken = default)
        => this.ResetPasswordAsync(context, cancellationToken);
        #endregion
    }
}

