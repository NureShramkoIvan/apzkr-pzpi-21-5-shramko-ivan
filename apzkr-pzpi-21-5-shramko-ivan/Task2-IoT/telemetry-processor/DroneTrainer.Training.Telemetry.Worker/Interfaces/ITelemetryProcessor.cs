namespace DroneTrainer.Training.Telemetry.Worker.Interfaces;

internal interface ITelemetryProcessor
{
    Task ProcessAsync(CancellationToken cancellationToken);
}
