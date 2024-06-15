using DroneTrainer.Infrastructure;
using System.Reflection;

namespace DroneTrainer.Api.DependencyModules;

public static class DependencyModule
{
    public static IServiceCollection AddDependencies(
        this IServiceCollection services,
        IConfigurationSection jwtSettings,
        string traimnerDBConnectionString,
        IConfigurationSection backupCreateSettings,
        IConfigurationSection backupReadSettings)
    {
        services.AddDocumentation();
        services.AddAuthentication(jwtSettings);
        services.AddInfrastructure(
            traimnerDBConnectionString,
            jwtSettings,
            backupCreateSettings,
            backupReadSettings);

        services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyModule))!);

        return services;
    }
}
