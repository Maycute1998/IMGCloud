using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure.Builders;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace IMGCloud.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly ICacheService _redisCache;
    private readonly IConfiguration _configuration;
    private readonly TokenOptions tokenOptions;

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
        _configuration = configuration;
        _redisCache = redisCache;
        this.tokenOptions = tokenOptions;
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
        var isExistUser = await _userService.IsActiveUserAsync(model, cancellationToken);
        if (isExistUser)
        {
            var userId = await _userService.GetUserIdByUserNameAsync(model.UserName, cancellationToken);
            var existedUserToken = await _userService.GetExistedTokenAsync(userId, cancellationToken);
            if (!string.IsNullOrWhiteSpace(existedUserToken))
            {

                var oldclaimsData = tokenBuilder.GetPrincipalFromExpiredToken(tokenOptions, existedUserToken);
                if (oldclaimsData is not null)
                {
                    var expiryDate = long.Parse(oldclaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                    var expDate = UnixTimeStampToDateTime(expiryDate);

                    var existedToken = IsExistedTokenExpired(existedUserToken, expDate);
                    // Case A: User is existed and token is not expired
                    if (!existedToken)
                    {
                        result.Token = existedUserToken;

                        StoreToken(model.UserName, existedUserToken, expDate);
                    }
                    //Case B: User is not existed or token is expired
                    else
                    {
                        result.Token = tokenBuilder.GenerateAccessToken(true).Value;

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
                var newClaimsData = tokenBuilder.GetPrincipalFromExpiredToken(tokenOptions, result.Token);
                var expiryDate = long.Parse(newClaimsData.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expDate = UnixTimeStampToDateTime(expiryDate);

                StoreToken(model.UserName, result.Token, expDate);

            }
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

    private void StoreToken(string userName, string userToken, DateTime expireDate)
    {
        var keyRedis = $"redis-{userName}";
        var token = new UserTokenContext()
        {
            UserName = userName,
            Token = userToken,
            ExpireDate = expireDate,
            IsActive = true
        };
        _userService.StoreTokenAsync(token);
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

