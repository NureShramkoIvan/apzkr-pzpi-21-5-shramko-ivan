using DroneTrainer.DataAccess.SqlServer.DbConnections;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DroneTrainer.DataAccess.SqlServer;

public static class DependencyModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(DependencyModule))!));

        services.AddTransient(_ => new DroneTrainerDbConnection(connectionString));
        return services;
    }
}
