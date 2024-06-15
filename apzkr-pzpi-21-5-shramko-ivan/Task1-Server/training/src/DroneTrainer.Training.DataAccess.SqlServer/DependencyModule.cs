using DroneTrainer.Training.DataAccess.SqlServer.DbConnections;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DroneTrainer.Training.DataAccess.SqlServer;

public static class DependencyModule
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string trainingDbConnectionString)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(DependencyModule))!));

        services.AddTransient(sp => new TrainingDbConnection(trainingDbConnectionString));

        return services;
    }
}
