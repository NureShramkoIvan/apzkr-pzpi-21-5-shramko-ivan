using DroneTrainer.Training.Infrastructure.DTOS;

namespace DroneTrainer.Training.Infrastructure.Interfaces;

public interface ITrainingResultService
{
    Task<UserTrainingSessionResultDTO> GetUserTrainingSessionResult(int userId, int sesisonId);
}
