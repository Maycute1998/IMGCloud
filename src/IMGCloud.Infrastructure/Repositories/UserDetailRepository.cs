using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    Task<UserDetail?> IUserDetailRepository.GetUserDetailbyUserIdAsync(int id, CancellationToken cancellationToken)
    => this.GetUserDetailbyUserIdAsync(id, cancellationToken);
}
