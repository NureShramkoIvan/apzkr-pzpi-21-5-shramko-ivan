using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using OneOf;

namespace DroneTrainer.Infrastructure.Interfaces;

public interface ITrainingSessionService
{
    Task<OneOf<
        int,
        TrainingProgramNotFoundError,
        InstructorNotFoundError,
        TrainingGroupNotFoundError>>
    CreateTrainingSessionAsync(TrainingSessionCreateDTO sessionCreateDTO);

    Task<OneOf<IEnumerable<TrainingSessionDTO>, UserNotFoundError>> GetUserTrainingSessions(int userId);
    Task<UserTrainingSessionResultDTO> GetUserTrainingSessionResult(int userId, int sessionId);
}
