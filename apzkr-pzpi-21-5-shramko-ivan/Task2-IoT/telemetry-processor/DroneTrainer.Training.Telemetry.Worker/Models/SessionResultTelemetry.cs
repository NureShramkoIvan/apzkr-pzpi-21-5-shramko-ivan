using System.Text.Json.Serialization;

namespace DroneTrainer.Training.Telemetry.Worker.Models;

internal sealed class SessionResultTelemetry : DeviceTelemetryBase
{
    [JsonPropertyName("session_id")]
    public int SessionId { get; set; }

    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("avarage_time_in_seconds")]
    public double AvarageTimeSeconds { get; set; }
}
