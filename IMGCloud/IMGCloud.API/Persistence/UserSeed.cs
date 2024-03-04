using IMGCloud.Data.Context;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.API.Persistence;

public static class UserSeed
{
    public static IHost SeedUp(this IHost app)
    {
        using var scope = app.Services.CreateScope();

        using var dbContext = scope.ServiceProvider
            .GetRequiredService<IMGCloudContext>();

        dbContext.Database.Migrate();

        if (!dbContext.Users.Any())
        {
            const string password = "admin123";
            dbContext.Users.Add(new()
            {
                UserName = "admin",
                Password = password.ToHashPassword(),
                CreatedDate = DateTime.UtcNow,
                Email = "admin@img.com"
            });

            dbContext.SaveChanges();
        }

        return app;
    }
}
