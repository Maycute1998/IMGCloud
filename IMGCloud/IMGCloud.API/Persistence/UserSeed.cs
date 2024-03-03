using IMGCloud.API.Extensions;
using IMGCloud.Data.Context;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.API.Persistence;

public static class UserSeed
{
    public static void SeedUp(string connectionSttring)
    {
        var services = new ServiceCollection();
        services.AddDbContext<IMGCloudContext>(opt => opt.UseSqlServer(connectionSttring));
        services.AddLogging();
        services.AddLocalization();
        services.AddScoped<IUserRepository, UserRepository>();
        var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        CreateUser(scope, new()
        {
            UserName = "admin",
            Password = "admin123",
            CreatedDate = DateTime.UtcNow,
            Email = "admin@img.com"
        });

    }

    private static void CreateUser(IServiceScope scope, User userInfo)
    {
        var repository = scope.ServiceProvider.GetService<IUserRepository>();
        if (repository is null)
        {
            return;
        }

        var user = repository.GetUserbyUserName("admin").Result;
        if (user is null)
        {
            var result = repository.CreateUserAsync(userInfo!.ToUserVM()).Result;
            if (result is null || !result.Status)
            {
                Console.WriteLine("Seed user is failed!!!");
            }
        }

    }
}
