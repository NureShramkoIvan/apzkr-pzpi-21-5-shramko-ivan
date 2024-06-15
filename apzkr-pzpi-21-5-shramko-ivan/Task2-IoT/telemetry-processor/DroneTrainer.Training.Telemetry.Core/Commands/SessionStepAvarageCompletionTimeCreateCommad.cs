using MediatR;

namespace DroneTrainer.Training.Telemetry.Core.Commands;

public sealed class SessionStepAvarageCompletionTimeCreateCommad(
    int sesisonId,
    string deviceId,
    double avgTime) : IRequest
{
    public int SesisonId { get; } = sesisonId;
    public string DeviceId { get; } = deviceId;
    public double AvgTime { get; } = avgTime;
}
