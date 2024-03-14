using IMGCloud.Infrastructure.Persistence;
using IMGCloud.API.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.ConfigureServices().ConfigurePipeline(builder.Configuration);
    app.SeedUp().Run();
}
catch (Exception ex)
{

}
