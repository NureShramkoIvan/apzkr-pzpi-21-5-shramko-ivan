using DroneTrainer.Mobile.Application.Pages;
using DroneTrainer.Mobile.Services;

namespace DroneTrainer.Mobile.Application;

public static class DependencyModule
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        services.AddServices();

        services.AddScoped<LoginPage>();
        services.AddScoped<HomePage>();
        services.AddScoped<SessionPage>();
        return services;
    }
}
