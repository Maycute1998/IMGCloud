using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Extensions;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.AutoMapper;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace IMGCloud.Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    public UserRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    private Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        dbContext.Users.Add(model.ToUser());
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    private Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return FindBy(x => x.UserName.ToLower() == userName.ToLower() && x.Status == Status.Active).FirstOrDefaultAsync(cancellationToken);
    }

    private async Task<int> GetUserIdAsync(string userName, CancellationToken cancellationToken)
    {
        var entity = await this.GetUserByUserNameAsync(userName, cancellationToken);
        return entity is not null ? entity.Id : default;
    }

    private async Task<bool> IsActiveUserAsync(string userName, CancellationToken cancellationToken)
    {
        var entity = await this.GetUserByUserNameAsync(userName, cancellationToken);
        return entity is not null && entity.Status == Status.Active || default(bool);
    }

    private async Task<bool> IsExitsUserEmailAsync(string email, CancellationToken cancellationToken)
    {
        var entity = await FindBy(x => x.Email.ToLower() == email.ToLower() && x.Status == Status.Active).FirstOrDefaultAsync(cancellationToken);
        return entity is not null;
    }

    public async Task<bool> IsExitsUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var entity = await this.GetUserByUserNameAsync(userName, cancellationToken);
        return entity is not null;
    }

    Task IUserRepository.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    => this.CreateUserAsync(model, cancellationToken);
    Task<User?> IUserRepository.GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.GetUserByUserNameAsync(userName, cancellationToken);
    Task<int> IUserRepository.GetUserIdByUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.GetUserIdAsync(userName, cancellationToken);
    Task<bool> IUserRepository.IsActiveUserAsync(string userName, CancellationToken cancellationToken)
    => this.IsActiveUserAsync(userName, cancellationToken);
    Task<bool> IUserRepository.IsExitsUserEmailAsync(string email, CancellationToken cancellationToken)
    => this.IsExitsUserEmailAsync(email, cancellationToken);
    Task<bool> IUserRepository.IsExitsUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.IsExitsUserNameAsync(userName, cancellationToken);
}
