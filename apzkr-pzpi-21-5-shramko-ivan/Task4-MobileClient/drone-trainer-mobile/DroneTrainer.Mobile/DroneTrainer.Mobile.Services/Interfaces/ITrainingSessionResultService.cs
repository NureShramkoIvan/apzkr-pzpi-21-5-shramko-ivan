using DroneTrainer.Mobile.Core.Models;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface ITrainingSessionResultService
{
    Task<UserTrainingSessionResult> GetUserTrainingSessionResult(int userId, int sessionId);
}
