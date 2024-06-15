using System.Text.Json.Serialization;

namespace DroneTrainer.Mobile.Core.Models;

public sealed class TrainingGroupParticipation
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    [JsonPropertyName("groupId")]
    public int GroupId { get; set; }
}
