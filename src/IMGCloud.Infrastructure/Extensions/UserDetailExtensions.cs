using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Extensions;

public static class UserDetailExtensions
{
    public static UserDetail ToUserDetail(this UserDetailsRequest request)
    => new()
    {
        UserId = request.UserId,
        PhoneNumber = request.PhoneNumber,
        FullName = request.FullName,
        Friend = request.Friend,
        Photo = request.Photo,
        Bio = request.Bio,
        Link = request.Link,
        Url = request.Url,
        BirthDay = request.Birthday,
    };
}