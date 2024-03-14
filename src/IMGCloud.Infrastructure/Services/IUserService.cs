using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IUserService
{
    Task<User> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<UserDetailContext?> GetUserDetailByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsActiveUserAsync(SignInContext user, CancellationToken cancellationToken = default);
    Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken = default);
    Task StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken = default);
    Task RemoveTokenAsync(CancellationToken cancellationToken = default);
    Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken = default);
    Task CreateUserDetailAsync(UserDetailsRequest userDetail, CancellationToken cancellationToken = default);
}
