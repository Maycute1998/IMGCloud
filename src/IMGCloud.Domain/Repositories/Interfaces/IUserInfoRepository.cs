using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using IMGCloud.Domain.Repositories.Implement;

namespace IMGCloud.Domain.Repositories.Interfaces
{
    public interface IUserInfoRepository
    {
        Task<UserInfo> GetUserInfobyId(int id);
    }
}
