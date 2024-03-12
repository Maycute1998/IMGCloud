﻿using IMGCloud.Utilities.Languages;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace IMGCloud.API.Extensions;

public static partial class WebApplicationExtensions
{
    public static WebApplication ConfigurePipeline(this WebApplication app, IConfiguration configuration)
    {
        if (app.Environment.IsDevelopment())
        {
            //
        }

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

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMGCloud.API");
        });
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("imgCloudCrossDomainOrigins");
        app.MapControllers();


        return app;
    }
}
