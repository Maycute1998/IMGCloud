using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Users
{
    public interface IUserService
    {
        int GetUserId(string userName);
        Task<ResponeVM> IsActiveUserAsync(SigInVM user);
        Task<ResponeVM> CreateUserAsync(CreateUserVM model);
    }
}
