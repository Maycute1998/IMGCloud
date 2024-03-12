namespace IMGCloud.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        Task<ResponeVM> SignUpAsync(UserVM model);
        Task<ResponeAuthVM> SignInAsync(SigInVM model);
        Task<ResponeVM> SignOutAsync();
    }
}
