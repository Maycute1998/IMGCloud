using IMGCloud.Application.Interfaces.Auth;
using IMGCloud.Application.Interfaces.Cache;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Models;
using IMGCloud.Utilities.TokenBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IMGCloud.Application.Implement.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IStringLocalizer<AuthenticationService> _stringLocalizer;
        private readonly ICacheService _redisCache;
        private readonly IConfiguration _configuration;

        public AuthenticationService(
              ILogger<AuthenticationService> logger
            , IUserService userService
            , IConfiguration configuration
            , IStringLocalizer<AuthenticationService> stringLocalizer
            , ICacheService redisCache)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
            _stringLocalizer = stringLocalizer;
            _redisCache = redisCache;
        }

        public async Task<ResponeAuthVM> SignInAsync(SigInVM model)
        {
            var result = new ResponeAuthVM();
            var tokenBuilder = new JwtTokenBuilder();
            tokenBuilder
                   .AddSecurityKey(JwtSecurityKey.Create(_configuration["TokenConfigs:SecurityKey"]))
                   .AddSubject(model.UserName)
                   .AddIssuer(_configuration["TokenConfigs:Issuer"])
                   .AddAudience(_configuration["TokenConfigs:Audience"])
                   .AddClaim(_configuration["TokenConfigs:ClaimKey"], _configuration["TokenConfigs:ClaimValue"])
                   .AddUserName(model.UserName)
                   .AddExpiryDate(int.Parse(_configuration["TokenConfigs:Expiry"]));
            try
            {
                var isExistUser = await _userService.IsActiveUserAsync(model);
                if (isExistUser is not null)
                {
                    result.Message = isExistUser.Message;
                    var userId = _userService.GetUserId(model.UserName);
                    if (userId != 0)
                    {
                        var existedUserToken = _userService.GetExistedTokenFromDatabase(userId);
                        if (!string.IsNullOrEmpty(existedUserToken))
                        {

                            var oldclaimsData = tokenBuilder.GetPrincipalFromExpiredToken(_configuration, existedUserToken);
                            if (oldclaimsData is not null)
                            {
                                var expiryDate = long.Parse(oldclaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                                var expDate = UnixTimeStampToDateTime(expiryDate);

                                var existedToken = IsExistedTokenExpired(existedUserToken, expDate);
                                // Case A: User is existed and token is not expired
                                if (!existedToken)
                                {
                                    result.Token = existedUserToken;
                                    result.Status = true;

                                    StoreToken(model.UserName, existedUserToken, expDate);
                                }
                                //Case B: User is not existed or token is expired
                                else
                                {
                                    result.Token = tokenBuilder.GenerateAccessToken(true).Value;
                                    result.Status = true;

                                    // Genarate new user token and store to database
                                    var userToken = tokenBuilder.GenerateAccessToken(true).Value;
                                    StoreToken(model.UserName, userToken, expDate);
                                }
                            }
                            else { _logger.LogError($"{nameof(SignInAsync)} Error: oldclaimsData is null"); }
                        }
                        else
                        {
                            result.Token = tokenBuilder.GenerateAccessToken(true).Value;
                            var newClaimsData = tokenBuilder.GetPrincipalFromExpiredToken(_configuration, result.Token);
                            var expiryDate = long.Parse(newClaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                            var expDate = UnixTimeStampToDateTime(expiryDate);

                            StoreToken(model.UserName, result.Token, expDate);
                            result.Status = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError($"Sigin {nameof(SignInAsync)}, Error: {_stringLocalizer["userNotFound"]}");
            }
            return result;
        }

        public async Task<ResponeVM> SignOutAsync()
        {
            var res = new ResponeVM();
            var data = _userService.RemveToken();
            res.Status = data.Status;
            res.Message = data.Message;
            return res;
        }

        public async Task<ResponeVM> SignUpAsync(UserVM model)
        {
            return await _userService.CreateUserAsync(model);
        }

        private bool IsExistedTokenExpired(string existedUserToken, DateTime expDate)
        {
            bool isExistedTokenExpired = false;
            try
            {
                if (!string.IsNullOrEmpty(existedUserToken))
                {
                    // Check the token we got if it is not expired
                    if (expDate < DateTime.UtcNow)
                    {
                        isExistedTokenExpired = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(IsExistedTokenExpired)} Error: {ex.Message}");
            }
            return isExistedTokenExpired;
        }
        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }

        private void StoreToken(string userName, string userToken, DateTime expireDate)
        {
            var keyRedis = $"redis-{userName}";
            var token = new TokenVM
            {
                UserName = userName,
                Token = userToken,
                ExpireDate = expireDate,
                IsActive = true
            };
            _userService.StoreTokenAsync(token);
            _redisCache.RemoveData(keyRedis);
            var redisData = new RedisAuthenticationVM
            {
                RedisKey = userName,
                Token = userToken,
                ExpireDate = expireDate
            };
            _redisCache.SetData(keyRedis, redisData, expireDate);
        }
    }
}
