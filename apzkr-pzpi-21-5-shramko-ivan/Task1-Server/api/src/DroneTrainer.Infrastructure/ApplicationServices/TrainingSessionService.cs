using AutoMapper;
using DroneTrainer.Core.Commands;
using DroneTrainer.Core.Enums;
using DroneTrainer.Core.Queries;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.Errors;
using DroneTrainer.Infrastructure.Interfaces;
using DroneTrainer.Shared.Dates.Services.Interfaces;
using DroneTrainer.Training.Service;
using MediatR;
using OneOf;
using static DroneTrainer.Training.Service.TrainingResult;

namespace DroneTrainer.Infrastructure.ApplicationServices;

internal sealed class TrainingSessionService(
    IMediator mediator,
    IMapper mapper,
    IDateConverterService dateConverterService,
    TrainingResultClient trainingResultClient) : ITrainingSessionService
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly IDateConverterService _dateConverterService = dateConverterService;
    private readonly TrainingResultClient _trainingResultClient = trainingResultClient;

    public async Task<OneOf<
        int,
        TrainingProgramNotFoundError,
        InstructorNotFoundError,
        TrainingGroupNotFoundError>>
    CreateTrainingSessionAsync(TrainingSessionCreateDTO sessionCreateDTO)
    {
        var program = await _mediator.Send(new TrainingProgramQuery(sessionCreateDTO.ProgramId));

        if (program is null) return new TrainingProgramNotFoundError();

        var instructor = await _mediator.Send(new UserByIdQuery(sessionCreateDTO.InstructorId));

        if (instructor is null || instructor.Role != Role.Instructor) return new InstructorNotFoundError();

        var group = await _mediator.Send(new TrainingGroupQuery(sessionCreateDTO.GroupId));

        if (group is null) return new TrainingGroupNotFoundError();

        var sessionId = await _mediator.Send(new TrainingSessionCreateCommand(
            sessionCreateDTO.ScheduledAt,
            sessionCreateDTO.GroupId,
            sessionCreateDTO.ProgramId,
            sessionCreateDTO.InstructorId));

        return sessionId;
    }

    public async Task<UserTrainingSessionResultDTO> GetUserTrainingSessionResult(int userId, int sessionId)
    {
        var userSessionResult = await _trainingResultClient.GetUserTrainingResultAsync(new UserTrainingResultRequest()
        {
            UserId = userId,
            SessionId = sessionId
        });

        return _mapper.Map<UserTrainingSessionResultDTO>(userSessionResult);
    }

    public async Task<OneOf<IEnumerable<TrainingSessionDTO>, UserNotFoundError>> GetUserTrainingSessions(int userId)
    {
        var user = await _mediator.Send(new UserByIdQuery(userId));

        if (user is null) return new UserNotFoundError();

        var userSessions = await _mediator.Send(new TrainingSessionsQuery(userId));

        foreach (var session in userSessions)
        {
            session.ScheduledAt = _dateConverterService.ConvertToSupportedTimeZoneDateTime(session.ScheduledAt, user.UserTimeZone);
            session.StartedAt = session.StartedAt is null
                ? null
                : _dateConverterService.ConvertToSupportedTimeZoneDateTime(session.StartedAt.Value, user.UserTimeZone);
            session.FinishedAt = session.FinishedAt is null
                ? null
                : _dateConverterService.ConvertToSupportedTimeZoneDateTime(session.FinishedAt.Value, user.UserTimeZone);
        }

        return userSessions.Select(_mapper.Map<TrainingSessionDTO>).ToList();
    }
}
