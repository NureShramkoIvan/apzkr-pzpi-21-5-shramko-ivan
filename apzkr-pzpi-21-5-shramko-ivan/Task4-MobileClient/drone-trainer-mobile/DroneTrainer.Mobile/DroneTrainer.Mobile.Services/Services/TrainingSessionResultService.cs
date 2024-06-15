using AutoMapper;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;
using static DroneTrainer.Mobile.Services.TrainingResult;

namespace DroneTrainer.Mobile.Services.Services;

internal class TrainingSessionResultService(TrainingResultClient trainingResultClient, IMapper mapper) : ITrainingSessionResultService
{
    private readonly TrainingResultClient _trainingResultClient = trainingResultClient;
    private readonly IMapper _mapper = mapper;

    public async Task<UserTrainingSessionResult> GetUserTrainingSessionResult(int userId, int sessionId)
    {
        var userSessionResult = await _trainingResultClient.GetUserTrainingResultAsync(new UserTrainingResultRequest()
        {
            UserId = userId,
            SessionId = sessionId
        });

        return _mapper.Map<UserTrainingSessionResult>(userSessionResult);
    }
}
