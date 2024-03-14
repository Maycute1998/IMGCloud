using IMGCloud.Domain.Entities;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserDetailRepository
{
    Task<UserDetail?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDetail?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
