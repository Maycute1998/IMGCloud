using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.Infrastructure.Repositories;

public sealed class UserTokenRepository : RepositoryBase<UserToken, int>, IUserTokenRepository
{
    public UserTokenRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
    {
    }

    private async Task<string?> GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
    {
        var userToken = await dbContext.UserTokens.Where(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        return userToken is not null ? userToken.Token : string.Empty;
    }

    public async Task RemoveTokenAsync(int userId, CancellationToken cancellationToken)
    {
        var userToken = await this.FindBy(x => x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        if (userToken is null)
        {
            return;
        }
        await this.DeleteAsync(userToken);
        await this.SaveChangesAsync(cancellationToken);
    }

    private async Task StoreToken(UserTokenContext context, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.Where(x => x.UserName.ToLower() == context.UserName.ToLower()).FirstOrDefaultAsync(cancellationToken);
        if (user is null)
        {
            return;
        }

        var userToken = await dbContext.UserTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync(cancellationToken);
        if (userToken is not null)
        {
            dbContext.UserTokens.Update(context.ToUserToken(userToken));
        }
        else
        {
            context.UserId = user.Id;
            dbContext.UserTokens.Add(context.ToUserToken());
        }

        await this.SaveChangesAsync(cancellationToken);
    }

    Task<string?> IUserTokenRepository.GetExistedTokenAsync(int userId, CancellationToken cancellationToken)
    => this.GetExistedTokenAsync(userId, cancellationToken);
    Task IUserTokenRepository.RemoveTokenAsync(int userId, CancellationToken cancellationToken)
    => this.RemoveTokenAsync(userId, cancellationToken);
    Task IUserTokenRepository.StoreTokenAsync(UserTokenContext context, CancellationToken cancellationToken)
    => this.StoreToken(context, cancellationToken);
}
