using System.Text.Json.Serialization;

namespace DroneTrainer.Training.Telemetry.Worker.Models;

internal sealed class StepPassTelemetry : DeviceTelemetryBase
{
    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; }

    [JsonPropertyName("passed_at")]
    public string PassedAt { get; set; }

    [JsonPropertyName("locale")]
    public string Locale { get; set; }

    [JsonPropertyName("attempt_id")]
    public int AttemptId { get; set; }
}
