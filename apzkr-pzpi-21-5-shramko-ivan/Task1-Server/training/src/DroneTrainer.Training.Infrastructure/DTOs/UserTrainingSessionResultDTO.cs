namespace DroneTrainer.Training.Infrastructure.DTOS;

public sealed class UserTrainingSessionResultDTO
{
    public double SuccessfullGatesPercent { get; set; }
    public double UnsuccessfullGatesPercent { get; set; }
    public TimeSpan SessionCompletionTime { get; set; }
    public double UserSuccessCoefficient { get; set; }
}
