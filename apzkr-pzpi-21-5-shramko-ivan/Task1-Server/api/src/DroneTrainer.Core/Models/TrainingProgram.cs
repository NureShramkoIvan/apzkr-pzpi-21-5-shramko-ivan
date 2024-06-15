namespace DroneTrainer.Core.Models;

public sealed class TrainingProgram
{
    public int Id { get; set; }
    public int OrganizationId { get; set; }
    public IEnumerable<TrainingProgramStep> Steps { get; set; }
}
