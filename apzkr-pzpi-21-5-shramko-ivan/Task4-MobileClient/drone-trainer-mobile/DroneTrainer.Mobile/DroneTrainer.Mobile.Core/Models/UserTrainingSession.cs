using System.Text.Json.Serialization;

namespace DroneTrainer.Mobile.Core.Models;

public sealed class UserTrainingSession
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("groupId")]
    public int GroupId { get; set; }

    [JsonPropertyName("programId")]
    public int ProgramId { get; set; }

    [JsonPropertyName("instructorId")]
    public int InstructorId { get; set; }

    [JsonPropertyName("scheduledAt")]
    public DateTime ScheduledAt { get; set; }

    [JsonPropertyName("startedAt")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("finishedAt")]
    public DateTime? FinishedAt { get; set; }
}
