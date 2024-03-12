using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;

namespace IMGCloud.Infrastructure.Extensions;

public static class UserTokenExtensions
{
    public static UserToken ToUserToken(this TokenContext context, UserToken userToken)
    => new()
    {
        UserId = userToken.UserId,
        Token = context.Token,
        Status = Status.Active,
        ExpiredDate = userToken.ExpiredDate,
        ModifiedDate = DateTime.UtcNow,
        CreatedDate = userToken.CreatedDate,
    };

    public static UserToken ToUserToken(this TokenContext context)
    => new()
    {
        UserId = context.UserId,
        Token = context.Token,
        ExpiredDate = context.ExpireDate,
        CreatedDate = DateTime.UtcNow,
        Status = context.IsActive ? Status.Active : Status.InActive,
    };
}
