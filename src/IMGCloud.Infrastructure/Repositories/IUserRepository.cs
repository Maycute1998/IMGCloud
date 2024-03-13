using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<bool> IsExitsUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> IsExitsUserEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User> CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken = default);
    Task<bool> IsActiveUserAsync(string userName, CancellationToken cancellationToken = default);
    Task<int> GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task CreateUserInfoAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default);
}
