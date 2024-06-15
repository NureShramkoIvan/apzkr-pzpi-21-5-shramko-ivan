namespace DroneTrainer.Training.Core.Models;

public sealed class TrainingSessionAttempt
{
    public int UserId { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
