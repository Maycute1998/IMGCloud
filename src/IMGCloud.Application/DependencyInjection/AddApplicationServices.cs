using IMGCloud.Application.Implement;
using IMGCloud.Application.Implement.Auth;
using IMGCloud.Application.Implement.Cache;
using IMGCloud.Application.Implement.Users;
using IMGCloud.Application.Interfaces;
using IMGCloud.Application.Interfaces.Auth;
using IMGCloud.Application.Interfaces.Cache;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Domain.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IMGCloud.Application.DependencyInjection
{
    public static class AddApplicationServices
    {
        public static void AddRepositoryApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
        }

        public static void AddServiceApplication(this IServiceCollection services)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

        }
    }
}
