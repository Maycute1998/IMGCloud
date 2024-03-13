using Google.Apis.Auth;
using IMGCloud.Application.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Application.Interfaces.Auth
{
    public interface IAuthExternalService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalAuthVM externalAuth);
    }
}
