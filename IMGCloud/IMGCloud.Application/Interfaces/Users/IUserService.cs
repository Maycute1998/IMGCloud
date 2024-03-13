using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;

namespace IMGCloud.Application.Interfaces.Users
{
    public interface IUserService
    {
        int GetUserId(string userName);
        Task<ResponeVM> IsActiveUserAsync(SigInVM user);
        Task<ResponeVM> IsExistEmailAsync(string email);
        Task<ResponeVM> CreateUserAsync(UserVM model);
        string GetExistedTokenFromDatabase(int userId);
        ResponeVM StoreTokenAsync(TokenVM tokenModel);
        ResponeVM RemveToken();
        Task<ResponeVM> GetUserInfor(string userName);

    }
}
