namespace DroneTrainer.Infrastructure.DTOs;

public sealed class TrainingProgramStepDTO
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int Position { get; set; }
    public int ProgramId { get; set; }
}
