using DroneTrainer.Training.DataAccess.SqlServer;
using DroneTrainer.Training.Infrastructure.Interfaces;
using DroneTrainer.Training.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DroneTrainer.Training.Infrastructure;

public static class DependencyModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string trainingDbConnection)
    {
        services.AddDataAccess(trainingDbConnection);

        services.AddScoped<ITrainingResultService, TrainingResultService>();
        return services;
    }
}
