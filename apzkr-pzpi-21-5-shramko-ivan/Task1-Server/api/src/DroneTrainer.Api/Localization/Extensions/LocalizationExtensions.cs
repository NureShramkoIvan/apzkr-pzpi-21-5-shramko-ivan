using DroneTrainer.Api.Localization.Constants;
using DroneTrainer.Api.Localization.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace DroneTrainer.Api.Localization.Extensions;

public static class LocalizationExtensions
{
    public static IServiceCollection AddResponseLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => options.ResourcesPath = "Resourses");
        services.AddTransient<ErrorMessageLocalizer>();

        return services;
    }

    public static WebApplication UseLocalization(this WebApplication app)
    {
        List<CultureInfo> supportedCultures = [new CultureInfo(Locales.EnUS), new CultureInfo(Locales.UkUA)];

        app.UseRequestLocalization(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(Locales.EnUS);
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders = [new AcceptLanguageHeaderRequestCultureProvider()];
            options.ApplyCurrentCultureToResponseHeaders = true;
        });

        return app;
    }
}
