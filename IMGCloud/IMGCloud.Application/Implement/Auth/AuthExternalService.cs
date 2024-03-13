using Google.Apis.Auth;
using IMGCloud.Application.Interfaces.Auth;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Application.ViewModels.Auth;
using Microsoft.Extensions.Configuration;

namespace IMGCloud.Application.Implement.Auth
{
    public class AuthExternalService : IAuthExternalService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _goolgeSettings;
        private readonly IUserService _userService;

        public AuthExternalService(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _goolgeSettings = _configuration.GetSection("GoogleAuthSettings");
            _userService = userService;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthVM externalAuth)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new List<string>()
                    {
                        _goolgeSettings.GetSection("clientId").Value
                    }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalAuth.GoogleTokenId, settings);

                var isExistUser = await _userService.IsExistEmailAsync(payload.Email);
                if(!isExistUser.Status)
                {
                    //await _userService.CreateUserAsync(payload.Name, "google", payload.Email);
                }

                // Add user to database
                //Build token

                return payload;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
