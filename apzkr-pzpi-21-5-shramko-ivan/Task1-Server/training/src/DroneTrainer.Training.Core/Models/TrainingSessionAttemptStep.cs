namespace DroneTrainer.Training.Core.Models;

public sealed class TrainingSessionAttemptStep
{
    public int ParticipantId { get; set; }
    public DateTime? PassedAt { get; set; }
    public int GateId { get; set; }
}
