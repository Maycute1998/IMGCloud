using IMGCloud.Infrastructure;
using IMGCloud.Utilities.Languages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace IMGCloud.API.Extensions;

public static partial class WebApplicationExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddLocalization();
        builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();
        builder.Services.AddSingleton<LocalizationMiddleware>();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        builder.Services.AddDbContext<ImgCloudContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectString")));
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddRepositoryApplication();
        builder.Services.AddServiceApplication();

        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureCors();

        return builder.Build();
    }

    internal static void AddRepositoryApplication(this IServiceCollection services)
    {

    }

    internal static void AddServiceApplication(this IServiceCollection services)
    {

    }

    internal static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            //opt.SaveToken = true;
            //opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,//Set Expiry
                ValidateIssuerSigningKey = true,
                SaveSigninToken = true,
                ClockSkew = TimeSpan.Zero,


                ValidIssuer = configuration["TokenConfigs:Issuer"],
                ValidAudience = configuration["TokenConfigs:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenConfigs:SecurityKey"]))
            };
        });
    }

    internal static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMGCloud API", Version = "v1" });
            _ = new Dictionary<string, IEnumerable<string>>
            {
                {
                    "Bearer", new string[] { }
                },
            };

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "IMGCloud API",
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
    }

    internal static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(o => o.AddPolicy(name: "imgCloudCrossDomainOrigins", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
    }
}
