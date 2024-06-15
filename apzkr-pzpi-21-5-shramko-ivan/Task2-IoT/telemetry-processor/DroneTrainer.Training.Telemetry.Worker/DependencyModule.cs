using DroneTrainer.Training.Telemetry.Inftrastructure;
using DroneTrainer.Training.Telemetry.Worker.Constants;
using DroneTrainer.Training.Telemetry.Worker.HostedServices;
using DroneTrainer.Training.Telemetry.Worker.Interfaces;
using DroneTrainer.Training.Telemetry.Worker.Services;
using System.Reflection;

namespace DroneTrainer.Training.Telemetry.Worker;

internal static class DependencyModule
{
    public static IHostApplicationBuilder ConfigureWorker(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddConsole();
        builder.Services.AddLogging();

        builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyModule))!);

        builder.Services.AddHostedService<TelemetryHostedService>();
        builder.Services.AddSingleton<ITelemetryProcessor, TelemetryProcessor>();

        builder.Services.AddInfrastructure(
            builder.Configuration[ConfigurationPaths.TrainingDb],
            builder.Configuration[ConfigurationPaths.ServiceBus],
            builder.Configuration.GetRequiredSection(ConfigurationPaths.TelemetryQueue));

        return builder;
    }
}
