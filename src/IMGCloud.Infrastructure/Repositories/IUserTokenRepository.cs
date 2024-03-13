using IMGCloud.Infrastructure.Context;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserTokenRepository
{
    Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken = default);
    Task StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken = default);
    Task RemoveTokenAsync(int userId, CancellationToken cancellationToken = default);
}
