using IMGCloud.API.Persistence;
using IMGCloud.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.ConfigureServices().ConfigurePipeline(builder.Configuration);
app.SeedUp().Run();