namespace DroneTrainer.Api.ViewModels;

public sealed class TrainingProgramStepVM
{
    public int Id { get; set; }
    public int DeviceId { get; set; }
    public int Position { get; set; }
    public int ProgramId { get; set; }
}
