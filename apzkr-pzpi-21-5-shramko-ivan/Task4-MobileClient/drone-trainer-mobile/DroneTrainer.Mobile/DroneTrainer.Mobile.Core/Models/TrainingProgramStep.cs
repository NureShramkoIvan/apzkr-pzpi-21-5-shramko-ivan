using System.Text.Json.Serialization;

namespace DroneTrainer.Mobile.Core.Models;

public sealed class TrainingProgramStep
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("deviceId")]
    public int? DeviceId { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("programId")]
    public int ProgramId { get; set; }

    [JsonPropertyName("deviceUniqueId")]
    public string DeviceUniqueId { get; set; }
}
