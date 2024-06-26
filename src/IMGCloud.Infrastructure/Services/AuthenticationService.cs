﻿using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure.Builders;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using static Amazon.S3.Util.S3EventNotification;

namespace IMGCloud.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly ICacheService _redisCache;
    private readonly TokenOptions tokenOptions;
    private readonly IStringLocalizer<AuthenticationService> _stringLocalizer;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
          ILogger<AuthenticationService> logger
        , IUserService userService
        , IConfiguration configuration
        , IStringLocalizer<AuthenticationService> stringLocalizer
        , ICacheService redisCache
        , TokenOptions tokenOptions)
    {
        _logger = logger;
        _userService = userService;
        _redisCache = redisCache;
        this.tokenOptions = tokenOptions;
        _stringLocalizer = stringLocalizer;
    }

    private async Task<AuthencationApiResult> SignInAsync(SignInContext model, CancellationToken cancellationToken)
    {
        var result = new AuthencationApiResult();
        var tokenBuilder = new JwtTokenBuilder();
        tokenBuilder
               .AddSecurityKey(JwtSecurityKey.Create(this.tokenOptions.SecurityKey))
               .AddSubject(model.UserName)
               .AddIssuer(this.tokenOptions.Issuer)
               .AddAudience(this.tokenOptions.Audience)
               .AddClaim(this.tokenOptions.ClaimKey, this.tokenOptions.ClaimValue)
               .AddUserName(model.UserName)
               .AddExpiryDate(this.tokenOptions.Expiry);

        var user = await _userService.GetUserByUserNameAsync(model.UserName, cancellationToken);
        if (user is not null)
        {
            var isCorrectPassword = model.Password.VerifyPassword(user.Password);
            if (!isCorrectPassword)
            {
                result.IsSucceeded = false;
                result.Message = _stringLocalizer["incorrectUserNameOrPassword"];
                return result;
            }
            else
            {
                var existedUserToken = await _userService.GetExistedTokenAsync(user.Id, cancellationToken);
                if (!string.IsNullOrWhiteSpace(existedUserToken))
                {

                    var oldclaimsData = tokenBuilder.GetPrincipalFromExpiredToken(tokenOptions, existedUserToken);
                    var expiryDate = long.Parse(oldclaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expDate = UnixTimeStampToDateTime(expiryDate);

                    if (oldclaimsData is not null)
                    {
                        var existedToken = IsExistedTokenExpired(existedUserToken, expDate);
                        if (!existedToken)
                        {
                            result.Token = existedUserToken;
                        }
                        else
                        {
                            result.Token = tokenBuilder.GenerateAccessToken(true).Value;
                            await StoreTokenAsync(model.UserName, result.Token, expDate);
                        }
                    }
                    else
                    {
                        result.Token = tokenBuilder.GenerateAccessToken(true).Value;
                        // Store new token to database
                        await StoreTokenAsync(model.UserName, result.Token, expDate);
                    }
                }
                else
                {
                    result.Token = tokenBuilder.GenerateAccessToken(true).Value;
                    var newClaimsData = tokenBuilder.GetPrincipalFromExpiredToken(tokenOptions, result.Token);
                    var expiryDate = long.Parse(newClaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expDate = UnixTimeStampToDateTime(expiryDate);
                    await StoreTokenAsync(model.UserName, result.Token, expDate);
                }
                result.IsSucceeded = true;
            }
        }
        else
        {
            result.Message = _stringLocalizer["accountNotExist"].ToString();
            result.IsSucceeded = false;
            result.Token = string.Empty;
        }
        return result;
    }

    private Task SignOutAsync(CancellationToken cancellationToken)
    => _userService.RemoveTokenAsync(cancellationToken);

    private Task SignUpAsync(CreateUserRequest model, CancellationToken cancellationToken)
    => _userService.CreateUserAsync(model, cancellationToken);

    private bool IsExistedTokenExpired(string existedUserToken, DateTime expDate)
    {
        bool isExistedTokenExpired = false;
        try
        {
            if (!string.IsNullOrWhiteSpace(existedUserToken) && expDate < DateTime.UtcNow)
            {
                isExistedTokenExpired = true;
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
        System.DateTime dtDateTime = DateTime.UnixEpoch;
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
        return dtDateTime;
    }

    private async Task StoreTokenAsync(string userName, string userToken, DateTime expireDate)
    {
        var token = new UserTokenContext()
        {
            UserName = userName,
            Token = userToken,
            ExpireDate = expireDate,
            IsActive = true
        };
        await _userService.StoreTokenAsync(token);

        var keyRedis = $"redis-{userName}";
        _redisCache.RemoveData(keyRedis);
        var redisData = new RedisContext()
        {
            RedisKey = userName,
            Token = userToken,
            ExpiredDate = expireDate
        };
        _redisCache.SetData(keyRedis, redisData, expireDate);
    }

    Task IAuthenticationService.SignUpAsync(CreateUserRequest model, CancellationToken cancellationToken)
    => this.SignUpAsync(model, cancellationToken);

    Task<AuthencationApiResult> IAuthenticationService.SignInAsync(SignInContext model, CancellationToken cancellationToken)
    => SignInAsync(model, cancellationToken);

    Task IAuthenticationService.SignOutAsync(CancellationToken cancellationToken)
    => this.SignOutAsync(cancellationToken);
}

