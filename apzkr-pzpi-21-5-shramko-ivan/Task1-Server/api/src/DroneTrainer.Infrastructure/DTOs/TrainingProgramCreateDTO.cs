namespace DroneTrainer.Infrastructure.DTOs;

public sealed class TrainingProgramCreateDTO
{
    public IEnumerable<TrainingProgramStepDTO> Steps { get; set; }

    public int OrganizationId { get; set; }
}
