using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.Infrastructure.Repositories;

public sealed class UserDetailRepository : RepositoryBase<UserDetail, int>, IUserDetailRepository
{
    public UserDetailRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    private Task<UserDetail?> GetUserDetailbyUserIdAsync(int id, CancellationToken cancellationToken)
    {
        return FindBy(x => x.UserId == id && x.Status == Status.Active).FirstOrDefaultAsync(cancellationToken);
    }

    private async Task<UserDetailContext?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var data = await dbContext.UserDetails
            .Include(x => x.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.User!.UserName == userName,cancellationToken);
        return new UserDetailContext
        {
            UserName = data.User!.UserName,
            Email = data.User.Email,
        }.MapFor(data);
    }

    Task<UserDetail?> IUserDetailRepository.GetByUserIdAsync(int id, CancellationToken cancellationToken)
    => this.GetUserDetailbyUserIdAsync(id, cancellationToken);

    Task<UserDetailContext?> IUserDetailRepository.GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.GetByUserNameAsync(userName, cancellationToken);
}
