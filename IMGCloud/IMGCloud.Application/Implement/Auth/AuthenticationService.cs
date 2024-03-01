using IMGCloud.Application.Interfaces.Auth;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IMGCloud.Application.Implement.Auth
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IStringLocalizer<AuthenticationService> _stringLocalizer;
        public AuthenticationService(ILogger<AuthenticationService> logger
            , IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public Task<Domain.Models.TokenResult> RefreshTokenAsync(string refreshToken, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponeAuthVM> SignInAsync(SigInVM model)
        {
            var result = new ResponeAuthVM();
            try
            {
                var isExistUser = await _userService.IsActiveUserAsync(model);
                if(isExistUser is not null)
                {
                    result.Message = isExistUser.Message;
                    var userId = _userService.GetUserId(model.UserName);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                _logger.LogError($"{nameof(SignInAsync)} Error: {_stringLocalizer["userNotFound"]}");
            }
            return result;
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Models.ResponeVM> SignUpAsync(CreateUserVM model)
        {
            return await _userService.CreateUserAsync(model);
        }

        #region Private Methods
        private ClaimsPrincipal GetToken(string token)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            return principal;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                var validIssuer = _configuration["TokenConfigs:Issuer"];
                var validAudience = _configuration["TokenConfigs:Audience"];
                var signingKey = _configuration["TokenConfigs:SecurityKey"];
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    SaveSigninToken = true,

                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };


                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);


                if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token");
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
