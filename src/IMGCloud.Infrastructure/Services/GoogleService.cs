using Google.Apis.Auth;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace IMGCloud.Infrastructure.Services;

public sealed class GoogleService : IGoogleService
{
    private readonly GoogleAuthenticationOptions options;
    private readonly IUserRepository userRepository;
    public GoogleService(GoogleAuthenticationOptions options, IUserRepository userRepository)
    {
        this.options = options;
        this.userRepository = userRepository;
    }

    private async Task<Payload> VerifyAsync(GoogleAuthenticationContext context, CancellationToken cancellationToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new List<string>()
            {
               options.ClientId
            }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(context.GoogleTokenId, settings);

        var isExistUser = await this.userRepository.IsExitsUserEmailAsync(payload.Email, cancellationToken);
        if (!isExistUser)
        {
            //await _userService.CreateUserAsync(payload.Name, "google", payload.Email);
        }

        // Add user to database
        //Build token

        return payload;
    }

    Task<Payload> IGoogleService.VerifyAsync(GoogleAuthenticationContext context, CancellationToken cancellationToken)
    => VerifyAsync(context, cancellationToken);
}
