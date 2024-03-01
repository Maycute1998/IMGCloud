using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using IMGCloud.Domain.Repositories.Implement;

namespace IMGCloud.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsExitsUserNameAsync(string userName);
        Task<bool> IsExitsUserEmailAsync(string email);
        Task<ResponeVM> CreateUserAsync(CreateUserVM model);
        Task<bool> IsActiveUserAsync(string userName);
        int GetUserId(string userName);
    }
}
