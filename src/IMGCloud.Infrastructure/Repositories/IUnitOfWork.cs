namespace IMGCloud.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
