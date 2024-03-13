using IMGCloud.Infrastructure.Context;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace IMGCloud.Infrastructure.Services;

public interface IGoogleService
{
    Task<Payload> VerifyTokenAsync(GoogleAuthenticationContext context, CancellationToken cancellationToken = default);
}
