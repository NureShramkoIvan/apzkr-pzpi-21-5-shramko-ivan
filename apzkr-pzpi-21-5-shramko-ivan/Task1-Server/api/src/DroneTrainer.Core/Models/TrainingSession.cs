namespace DroneTrainer.Core.Models;

public sealed class TrainingSession
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public int ProgramId { get; set; }
    public int InstructorId { get; set; }
    public DateTime ScheduledAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
