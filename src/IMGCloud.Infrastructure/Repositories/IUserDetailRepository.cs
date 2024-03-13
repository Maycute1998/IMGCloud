using IMGCloud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Repositories;

public interface IUserDetailRepository
{
    Task<UserDetail?> GetUserDetailbyUserIdAsync(int id, CancellationToken cancellationToken = default);
    Task<UserDetail?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
}
