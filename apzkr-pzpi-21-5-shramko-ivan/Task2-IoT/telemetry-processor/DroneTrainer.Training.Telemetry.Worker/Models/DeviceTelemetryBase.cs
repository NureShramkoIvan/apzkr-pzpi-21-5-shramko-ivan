using System.Text.Json.Serialization;

namespace DroneTrainer.Training.Telemetry.Worker.Models;

public abstract class DeviceTelemetryBase
{
    [JsonPropertyName("message_type")]
    public string MessageType { get; set; }
}
