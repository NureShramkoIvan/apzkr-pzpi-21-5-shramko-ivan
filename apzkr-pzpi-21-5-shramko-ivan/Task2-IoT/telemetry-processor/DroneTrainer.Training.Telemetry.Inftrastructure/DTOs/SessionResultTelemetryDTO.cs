namespace DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;

public class SessionResultTelemetryDTO
{
    public int SessionId { get; set; }
    public string DeviceId { get; set; }
    public double AvarageTimeSeconds { get; set; }
}
