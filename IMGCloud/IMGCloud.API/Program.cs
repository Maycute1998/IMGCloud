using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using IMGCloud.Utilities.Languages;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using IMGCloud.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IMGCloud.Application.DependencyInjection;
using IMGCloud.API.Persistence;
using Amazon.S3;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var crossDomainOrigins = "imgCloudCrossDomainOrigins";

#region[Localization]
builder.Services.AddLocalization();
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();
builder.Services.AddSingleton<LocalizationMiddleware>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
#endregion

builder.Services.AddRepositoryApplication();
builder.Services.AddControllers();
builder.Services.AddDbContext<IMGCloudContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectString")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServiceApplication();

#region[AWSService]
var accessKey = configuration["AWS:Credentials:AccessKey"];
var secretAccessKey = configuration["AWS:Credentials:SecretKey"];
var awsOptions = new AWSOptions
{
    Credentials = new BasicAWSCredentials(accessKey, secretAccessKey),
    Region = RegionEndpoint.APSoutheast2
};
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonS3>();
#endregion

#region JWT Config
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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


        ValidIssuer = configuration["TokenConfigs:Issuer"],
        ValidAudience = configuration["TokenConfigs:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenConfigs:SecurityKey"]))
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
#endregion

#region [Config-Cors-Domain]
builder.Services.AddCors(o => o.AddPolicy(name: crossDomainOrigins, builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));
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
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(crossDomainOrigins);
app.MapControllers();

app.SeedUp().Run();