using DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;

namespace DroneTrainer.Training.Telemetry.Inftrastructure.Interfaces;

public interface IDeviceTelemetryService
{
    Task SaveStepCompletionTelemetry(StepPassTelemetryDTO telemetry);
    Task SaveSessionResultTelemetry(SessionResultTelemetryDTO telemetry);
}
