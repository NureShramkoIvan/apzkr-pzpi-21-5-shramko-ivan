using DroneTrainer.Training.Telemetry.Core.Commands;
using DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;
using DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;
using MediatR;

namespace DroneTrainer.Training.Telemetry.Inftrastructure.Services;

internal sealed class DeviceTelemetryService(IMediator mediator) : IDeviceTelemetryService
{
    private readonly IMediator _mediator = mediator;

    public async Task SaveSessionResultTelemetry(SessionResultTelemetryDTO telemetry)
    {
        await _mediator.Send(new SessionStepAvarageCompletionTimeCreateCommad(
            telemetry.SessionId,
            telemetry.DeviceId,
            telemetry.AvarageTimeSeconds));
    }

    public async Task SaveStepCompletionTelemetry(StepPassTelemetryDTO telemetry)
    {
        await _mediator.Send(new StepCompletionCommand(
            telemetry.PassedAt,
            telemetry.DeviceId,
            telemetry.AttemptId));
    }
}
