using AutoMapper;
using DroneTrainer.Training.Infrastructure.Interfaces;
using Grpc.Core;

namespace DroneTrainer.Training.Service.Services;

public class TrainingResultService(Infrastructure.Interfaces.ITrainingResultService trainingResultService, IMapper mapper) : TrainingResult.TrainingResultBase
{
    private readonly ITrainingResultService _trainingResultService = trainingResultService;
    private readonly IMapper _mapper = mapper;

    public override async Task<UserTrainingResultResponse> GetUserTrainingResult(UserTrainingResultRequest request, ServerCallContext context)
    {
        var trainingResult = await _trainingResultService.GetUserTrainingSessionResult(request.UserId, request.SessionId);

        return _mapper.Map<UserTrainingResultResponse>(trainingResult);
    }
}
