using DroneTrainer.Training.Telemetry.Worker.Interfaces;

namespace DroneTrainer.Training.Telemetry.Worker.HostedServices;

internal sealed class TelemetryHostedService(ITelemetryProcessor telemetryProcessor) : BackgroundService
{
    private readonly ITelemetryProcessor _telemetryProcessor = telemetryProcessor;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _telemetryProcessor.ProcessAsync(cancellationToken);
    }
}
