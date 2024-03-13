using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IAuthenticationService
{
    Task<ApiResult<User>> SignUpAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<AuthencationResult> SignInAsync(SignInContext model, CancellationToken cancellationToken = default);
    Task SignOutAsync(CancellationToken cancellationToken = default);
}
