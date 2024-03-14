using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Services;
using IMGCloud.Utilities.Languages;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace IMGCloud.API.Extensions;

public static partial class WebApplicationExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddDbContext<ImgCloudContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectString")));
        builder.Services.AddApplicationOptions(builder.Configuration);

        builder.Services.AddRepositoryApplication();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        builder.Services.AddLocalization();
        builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();
        builder.Services.AddSingleton<LocalizationMiddleware>();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();


        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.ConfigureAmazonS3(builder.Configuration);

        builder.Services.AddServiceApplication();

        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureCors();

        return builder.Build();
    }

    internal static void AddRepositoryApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserTokenRepository, UserTokenRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
    }

    internal static void AddServiceApplication(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IGoogleService, GoogleService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IAmazonBucketService, AmazonBucketService>();
    }

    internal static void AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var amazonBulket = configuration.GetSection(nameof(AmazonBulketOptions)).Get<AmazonBulketOptions>();
        ArgumentNullException.ThrowIfNull(amazonBulket);
        services.AddSingleton(amazonBulket);

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        ArgumentNullException.ThrowIfNull(jwtOptions);
        services.AddSingleton(jwtOptions);

        var tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
        ArgumentNullException.ThrowIfNull(tokenOptions);
        services.AddSingleton(tokenOptions);

        var googleAuthenticationOptions = configuration.GetSection(nameof(GoogleAuthenticationOptions)).Get<GoogleAuthenticationOptions>();
        ArgumentNullException.ThrowIfNull(googleAuthenticationOptions);
        services.AddSingleton(googleAuthenticationOptions);

        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

    }

    internal static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>();
        ArgumentNullException.ThrowIfNull(tokenOptions);
        //var provider = services.BuildServiceProvider();
        //var tokenOptions = provider.GetRequiredService<TokenOptions>();
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,//Set Expiry
                ValidateIssuerSigningKey = true,
                SaveSigninToken = true,
                ClockSkew = TimeSpan.Zero,


                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
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

    internal static void ConfigureAmazonS3(this IServiceCollection services, IConfiguration configuration)
    {
        var amazonS3Options = configuration.GetSection(nameof(AmazonBulketOptions)).Get<AmazonBulketOptions>();
        var awsOptions = new AWSOptions
        {
            Credentials = new BasicAWSCredentials(amazonS3Options.AccessKey, amazonS3Options.SecretAccessKey),
            Region = RegionEndpoint.APSoutheast2
        };
        services.AddDefaultAWSOptions(awsOptions);
        services.AddAWSService<IAmazonS3>();
    }
}
