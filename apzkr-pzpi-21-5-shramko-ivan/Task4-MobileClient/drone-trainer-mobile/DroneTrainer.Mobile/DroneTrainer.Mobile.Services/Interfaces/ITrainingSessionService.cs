using DroneTrainer.Mobile.Core.Models;

namespace DroneTrainer.Mobile.Services.Interfaces;

public interface ITrainingSessionService
{
    Task StartTrainingSessionAsync(
        int sessionId,
        IEnumerable<int> userIds,
        IEnumerable<string> deviceUniqueIds,
        string locale);

    Task EndTrainingSessionAsync(int sessionId, IEnumerable<string> deviceUniqueIds);
    Task StartTrainingSessionAttemptAsync(int sessionId, int attemptId, IEnumerable<string> deviceUniqueIds);
    Task EndTrainingSessionAttemptAsync(int sessionId, int attemptId, IEnumerable<string> deviceUniqueIds);
    Task<IEnumerable<UserTrainingSession>> GetUserTrainingSessions(int userId);
    Task<IEnumerable<Core.Models.UserAttempt>> GetUserAttemptIds(IEnumerable<int> userIds, int sessionId);
}
