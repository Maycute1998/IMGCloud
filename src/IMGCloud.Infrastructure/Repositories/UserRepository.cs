using Amazon.Runtime.Internal;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Extensions;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.Infrastructure.Repositories;

public sealed class UserRepository : RepositoryBase<User, int>, IUserRepository
{
    public UserRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    private async Task CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        var user = model.ToUser();
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        model.UserName = user.UserName;
    }

    private Task<User?> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        return FindBy(x => x.UserName.ToLower() == userName.ToLower() 
                        && x.Status == Status.Active).FirstOrDefaultAsync(cancellationToken);
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

    public async Task CreateUserDetailAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
    {
        var userDetail = new UserDetail().MapFor(userInfo);
        var user = await GetUserByUserNameAsync(userInfo.UserName, cancellationToken);
        if (user is not null)
        {
            userDetail.UserId = user!.Id;
            userDetail.CreatedDate = DateTime.Now;
            userDetail.ModifiedDate = DateTime.Now;
            userDetail.Status = Status.Active;
            dbContext.UserDetails.Add(userDetail);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    Task IUserRepository.CreateUserAsync(CreateUserRequest model, CancellationToken cancellationToken)
    => this.CreateUserAsync(model, cancellationToken);
    Task<User?> IUserRepository.GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.GetUserByUserNameAsync(userName, cancellationToken);
    Task<bool> IUserRepository.IsActiveUserAsync(string userName, CancellationToken cancellationToken)
    => this.IsActiveUserAsync(userName, cancellationToken);
    Task<bool> IUserRepository.IsExitsUserEmailAsync(string email, CancellationToken cancellationToken)
    => this.IsExitsUserEmailAsync(email, cancellationToken);
    Task<bool> IUserRepository.IsExitsUserNameAsync(string userName, CancellationToken cancellationToken)
    => this.IsExitsUserNameAsync(userName, cancellationToken);

    Task IUserRepository.CreateUserDetailAsync(UserDetailsRequest userInfo, CancellationToken cancellationToken)
    =>this.CreateUserDetailAsync(userInfo, cancellationToken);
}
