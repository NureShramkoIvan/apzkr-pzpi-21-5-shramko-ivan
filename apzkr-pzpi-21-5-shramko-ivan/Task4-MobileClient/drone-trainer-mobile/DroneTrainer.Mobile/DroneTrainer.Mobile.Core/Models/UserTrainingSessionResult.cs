namespace DroneTrainer.Mobile.Core.Models;

public sealed class UserTrainingSessionResult
{
    public double SuccessfullGatesPercent { get; set; }
    public double UnsuccessfullGatesPercent { get; set; }
    public TimeSpan SessionCompletionTime { get; set; }
    public double UserSuccessCoefficient { get; set; }
}
