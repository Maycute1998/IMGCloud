namespace IMGCloud.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ImgCloudContext dbContext;
    public UnitOfWork(ImgCloudContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    => dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose()
    {
        dbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork() // Noncompliant - Modify UnitOfWork.~UnitOfWork() so that it calls Dispose(false) and then returns.
    {
        // Cleanup
    }
}
