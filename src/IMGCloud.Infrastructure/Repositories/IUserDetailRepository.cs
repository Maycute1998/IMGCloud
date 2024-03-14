using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserDetailRepository
{
    Task<UserDetail?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDetailContext?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
