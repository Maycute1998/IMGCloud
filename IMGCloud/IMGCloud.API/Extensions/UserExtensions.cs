using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;

namespace IMGCloud.API.Extensions
{
    public static class UserExtensions
    {
        public static UserVM ToUserVM(this User? user)
        => new()
        {
            Email = user?.Email,
            Password = user?.Password,
            UserName = user?.UserName
        };
    }
}
