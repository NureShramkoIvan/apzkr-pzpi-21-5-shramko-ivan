namespace DroneTrainer.Infrastructure.DTOs;

public sealed class TrainingSessionCreateDTO
{
    public DateTime ScheduledAt { get; set; }
    public int ProgramId { get; set; }
    public int InstructorId { get; set; }
    public int GroupId { get; set; }
}
