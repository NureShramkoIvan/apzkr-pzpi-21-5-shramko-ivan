using MediatR;

namespace DroneTrainer.Training.Telemetry.Core.Commands;

public sealed class StepCompletionCommand(
    DateTime passedAt,
    string deviceId,
    int attemptId) : IRequest
{
    public DateTime PassedAt { get; } = passedAt;
    public string DeviceId { get; } = deviceId;
    public int AttemptId { get; } = attemptId;
}
