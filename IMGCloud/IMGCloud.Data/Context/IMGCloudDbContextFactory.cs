using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IMGCloud.Data.Context
{
    public class IMGCloudDbContextFactory : IDesignTimeDbContextFactory<IMGCloudContext>
    {
        public IMGCloudContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("IMGCloudDb");
            var optionsBuilder = new DbContextOptionsBuilder<IMGCloudContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new IMGCloudContext(optionsBuilder.Options);
        }
    }
}
