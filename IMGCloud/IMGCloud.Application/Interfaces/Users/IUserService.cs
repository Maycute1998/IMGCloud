using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Users
{
    public interface IUserService
    {
        Task<ResponeVM> CreateUserAsync(CreateUserVM model);
    }
}
