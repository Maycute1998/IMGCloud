using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
var enviroments = !string.IsNullOrEmpty(configuration["Enviroments"]) ? configuration["Enviroments"] : null;
builder.Configuration.AddJsonFile($"ocelot.{enviroments}.json", optional: true, reloadOnChange: true);
builder.Services.AddOcelot();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseOcelot().Wait();
app.MapGet("/", () => $"Gateway run successfully {configuration["Enviroments"]} Enviroment");
app.Run();