namespace DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;

public class StepPassTelemetryDTO
{
    public string DeviceId { get; set; }
    public DateTime PassedAt { get; set; }
    public int AttemptId { get; set; }
}
