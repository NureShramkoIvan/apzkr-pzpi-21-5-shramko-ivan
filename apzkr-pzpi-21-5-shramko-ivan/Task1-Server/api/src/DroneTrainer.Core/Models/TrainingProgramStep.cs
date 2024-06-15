namespace DroneTrainer.Core.Models;

public sealed class TrainingProgramStep
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int Position { get; set; }
    public int ProgramId { get; set; }
}
