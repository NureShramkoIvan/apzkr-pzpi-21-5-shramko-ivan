using System.Text.Json.Serialization;

namespace DroneTrainer.Mobile.Core.Models;

public sealed class TrainingProgram
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("organizationId")]
    public int OrganizationId { get; set; }

    [JsonPropertyName("organizationName")]
    public string OrganizationName { get; set; }

    [JsonPropertyName("steps")]
    public IEnumerable<TrainingProgramStep> Steps { get; set; }
}
