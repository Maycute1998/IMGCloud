using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Utilities.PasswordHashExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Extensions;

public static class UserExtensions
{
    public static User ToUser(this CreateUserRequest request)
    => new()
    {
        UserName = request.UserName,
        Password = request.Password.ToHashPassword(),
        Status = Status.Active,
        Email = request.Email,
        CreatedDate = DateTime.UtcNow,
        ModifiedDate = DateTime.UtcNow,
    };
}
