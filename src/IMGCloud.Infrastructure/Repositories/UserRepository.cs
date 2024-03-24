using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
        var entity = userInfo.ToUserDetail();
        var user = await GetUserByUserNameAsync(userInfo.UserName, cancellationToken);
        if (user is not null)
        {
            entity.UserId = user!.Id;
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.Status = Status.Active;
            dbContext.UserDetails.Add(entity);
            await this.SaveChangesAsync(cancellationToken);
            userInfo.Id = entity.Id;
        }
    }

    public async Task<string> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default)
    {
        string resetToken = string.Empty;
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
        if (user is not null)
        {
            resetToken = Guid.NewGuid().ToString();

            user.ResetPasswordToken = resetToken;
            user.ResetPasswordTokenExpiration = DateTime.UtcNow.AddMinutes(5);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        return resetToken;
    }

    public async Task ResetPasswordAsync(ResetPasswordContext context, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.ResetPasswordToken == context.Token);

        if (user is not null)
        {
            if (user.ResetPasswordTokenExpiration < DateTime.UtcNow)
            {
                user.Password = context.NewPassword.ToHashPassword();
                user.ResetPasswordToken = null;
                user.ResetPasswordTokenExpiration = null;
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("Token expired");
            }
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

    Task<string> IUserRepository.ForgotPasswordAsync(string email, CancellationToken cancellationToken)
    => this.ForgotPasswordAsync(email, cancellationToken);

    Task IUserRepository.ResetPasswordAsync(ResetPasswordContext context, CancellationToken cancellationToken)
    => this.ResetPasswordAsync(context, cancellationToken);
}
