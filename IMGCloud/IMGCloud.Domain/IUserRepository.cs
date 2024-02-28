using IMGCloud.Domain.Models;

namespace IMGCloud.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> IsExitsUserNameAsync(string userName);
        Task<bool> IsExitsUserEmailAsync(string email);
        Task<ResponeVM> CreateUserAsync(CreateUserVM model);

    }
}
