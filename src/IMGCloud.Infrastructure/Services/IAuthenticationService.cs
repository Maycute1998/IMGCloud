using IMGCloud.Domain.Cores;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IAuthenticationService
{
    Task SignUpAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<AuthencationApiResult> SignInAsync(SignInContext model, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
}
