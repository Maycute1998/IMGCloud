using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IUserService
{
    Task<int> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsActiveUserAsync(SignInContext user, CancellationToken cancellationToken = default);
    Task<ApiResult<User>> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken = default);
    Task StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken = default);
    Task RemoveTokenAsync(CancellationToken cancellationToken = default);
    Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> IsExistEmailAsync(string email);
    Task<ApiResult> CreateUserInfoAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default);

}
