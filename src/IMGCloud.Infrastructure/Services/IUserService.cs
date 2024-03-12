namespace IMGCloud.Infrastructure.Services;

public interface IUserService
{
    int GetUserId(string userName);
    Task<ResponeVM> IsActiveUserAsync(SigInVM user);
    Task<ResponeVM> CreateUserAsync(UserVM model);
    string GetExistedTokenFromDatabase(int userId);
    ResponeVM StoreTokenAsync(TokenVM tokenModel);
    ResponeVM RemveToken();
    Task<ResponeVM> GetUserInfor(string userName);

}
