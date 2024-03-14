using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IMGCloud.Infrastructure.Persistence;

public static class UserSeed
{
    public static IHost SeedUp(this IHost app)
    {
        using var scope = app.Services.CreateScope();

        using var dbContext = scope.ServiceProvider
            .GetRequiredService<ImgCloudContext>();

        dbContext.Database.Migrate();

        if (!dbContext.Users.Any())
        {
            const string password = "admin123";
            dbContext.Users.Add(new()
            {
                UserName = "admin",
                Password = password.ToHashPassword(),
                CreatedDate = DateTime.UtcNow,
                Email = "admin@img.com",
                Status = Domain.Entities.Status.Active,
            });

            dbContext.SaveChanges();
        }

        return app;
    }
}
