using IMGCloud.Application.Implement.Auth;
using IMGCloud.Application.Implement.Users;
using IMGCloud.Application.Interfaces.Auth;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using IMGCloud.Utilities.Languages;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Data.Context;
using Microsoft.EntityFrameworkCore;
using IMGCloud.Application.Interfaces.Users;
using IMGCloud.Application.Interfaces.Cache;
using IMGCloud.Application.Implement.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region[Localization]
builder.Services.AddLocalization();
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
#endregion

builder.Services.AddControllers();
builder.Services.AddDbContext<IMGCloudContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectString")));
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

#region JWT Config
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
#endregion

#region[Swagger]
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMGCloud API", Version = "v1" });
    _ = new Dictionary<string, IEnumerable<string>>
    {
        {"Bearer", new string[] { }},
    };

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description ="IMGCloud API",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region[Localization]
var defaultValue = configuration.GetSection("Localization:Default").Value;
if (string.IsNullOrEmpty(defaultValue))
{
    defaultValue = "en-US";
}
var options = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(new CultureInfo(defaultValue))
};
app.UseRequestLocalization(options);
app.UseStaticFiles();
app.UseMiddleware<LocalizationMiddleware>();
#endregion

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMGCloud.API");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
