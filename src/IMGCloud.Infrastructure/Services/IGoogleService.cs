using IMGCloud.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace IMGCloud.Infrastructure.Services;

public interface IGoogleService
{
    Task<Payload> VerifyAsync(GoogleAuthenticationContext context, CancellationToken cancellationToken = default);
}
