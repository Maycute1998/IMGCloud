using Microsoft.Extensions.Configuration;
using System.IO;

namespace IMGCloud.Utilities.RedisConfig
{
    static class RedisConfiguration
    {
        public static IConfiguration AppSetting
        {
            get;
        }

        static RedisConfiguration()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
        }
    }
}
