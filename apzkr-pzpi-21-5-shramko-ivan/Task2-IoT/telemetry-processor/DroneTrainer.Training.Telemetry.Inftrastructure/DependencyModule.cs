using Azure.Messaging.ServiceBus;
using DroneTrainer.Training.Telemetry.DataAccess.SqlServer;
using DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;
using DroneTrainer.Training.Telemetry.Inftrastructure.Services;
using DroneTrainer.Training.Telemetry.Inftrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DroneTrainer.Training.Telemetry.Inftrastructure;

public static class DependencyModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string trainingDbConnectionString,
        string serviceBusConnectionString,
        IConfigurationSection deviceTelemetryQueueSettings)
    {
        services.AddDataAccess(trainingDbConnectionString);

        services.Configure<DeviceTelemetryQueueSettings>(deviceTelemetryQueueSettings);

        services.AddSingleton<IQueueService, QueueService>();
        services.AddSingleton<IDeviceTelemetryService, DeviceTelemetryService>();

        services.AddSingleton(sp => new ServiceBusClient(serviceBusConnectionString));
        return services;
    }
}
