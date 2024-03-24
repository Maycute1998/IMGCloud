using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<bool> IsExitsUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsExitsUserEmailAsync(string email, CancellationToken cancellationToken = default);
    Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<bool> IsActiveUserAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task CreateUserDetailAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default);
    Task<string> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task ResetPasswordAsync(ResetPasswordRequest context, CancellationToken cancellationToken = default);
}
