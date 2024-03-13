using IMGCloud.API.Extensions;
using IMGCloud.API.Persistence;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.ConfigureServices().ConfigurePipeline(builder.Configuration);
    app.SeedUp().Run();
}
catch (Exception ex)
{

}
