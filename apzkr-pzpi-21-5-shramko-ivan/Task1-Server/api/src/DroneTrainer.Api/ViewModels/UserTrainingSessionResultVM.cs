namespace DroneTrainer.Api.ViewModels;

public class UserTrainingSessionResultVM
{
    public double SuccessfullGatesPercent { get; set; }
    public double UnsuccessfullGatesPercent { get; set; }
    public TimeSpan SessionCompletionTime { get; set; }
    public double UserSuccessCoefficient { get; set; }
}