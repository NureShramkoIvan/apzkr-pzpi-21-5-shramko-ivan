using DroneTrainer.Training.Core.Queries;
using DroneTrainer.Training.Infrastructure.DTOS;
using DroneTrainer.Training.Infrastructure.Interfaces;
using MediatR;

namespace DroneTrainer.Training.Infrastructure.Services;

internal sealed class TrainingResultService(IMediator mediator) : ITrainingResultService
{
    private readonly IMediator _mediator = mediator;

    public async Task<UserTrainingSessionResultDTO> GetUserTrainingSessionResult(int userId, int sesisonId)
    {
        var sessionAttempts = await _mediator.Send(new TrainingSessionAttemptsQuery(sesisonId));
        var sessionAttempSteps = await _mediator.Send(new TrainingSessionAttemptStepsQuery(sesisonId));

        var userSessionAttemptSteps = sessionAttempSteps.Where(r => r.ParticipantId == userId);
        var selectedUserSessionAttempt = sessionAttempts.Where(a => a.UserId == userId).Single();

        var successfullGatesPercent = (double)userSessionAttemptSteps.Where(r => r.PassedAt is not null).Count() / (double)userSessionAttemptSteps.Count();
        var unsuccesfullGatesPercent = (double)userSessionAttemptSteps.Where(r => r.PassedAt is null).Count() / (double)userSessionAttemptSteps.Count();

        var selectedUserSessionCompletionTime = userSessionAttemptSteps.Aggregate(
            TimeSpan.Zero,
            (acc, item) =>
            {
                acc += item.PassedAt is not null
                    ? item.PassedAt.Value - selectedUserSessionAttempt.StartedAt.Value - acc
                    : TimeSpan.Zero;

                return acc;
            });

        var medianlist = sessionAttempSteps
            .GroupBy(i => i.ParticipantId)
            .Join(
                sessionAttempts,
                usas => usas.Key,
                sa => sa.UserId,
                (usas, sa) => new
                {
                    ParticipantId = usas.Key,
                    SuccessfullGatesPercent =
                        (double)usas.Where(r => r.PassedAt is not null).Count()
                        / (double)usas.Count(),
                    GatesCount = usas.Count(),
                    TotalTime = usas.Aggregate(
                    TimeSpan.Zero,
                    (acc, item) =>
                    {
                        acc += item.PassedAt is not null
                            ? item.PassedAt.Value - sa.StartedAt.Value - acc
                            : TimeSpan.Zero;

                        return acc;
                    })
                })
            .Select(t => t.TotalTime + (t.TotalTime / t.SuccessfullGatesPercent / t.GatesCount * (1 - t.SuccessfullGatesPercent)))
            .Order()
            .ToList();

        var median = medianlist.Count % 2 == 0
            ? (medianlist.ElementAt((medianlist.Count / 2) + 1) + medianlist.ElementAt((medianlist.Count / 2) - 1)) / 2
            : medianlist.ElementAt((int)((medianlist.Count / 2) + 0.5));

        var b = selectedUserSessionCompletionTime + (selectedUserSessionCompletionTime / successfullGatesPercent / userSessionAttemptSteps.Count() * unsuccesfullGatesPercent);
        var successIndex = median / b;

        return new()
        {
            SessionCompletionTime = selectedUserSessionCompletionTime,
            SuccessfullGatesPercent = successfullGatesPercent,
            UnsuccessfullGatesPercent = unsuccesfullGatesPercent,
            UserSuccessCoefficient = successIndex
        };
    }
}
