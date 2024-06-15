using DroneTrainer.Training.Core.Models;
using MediatR;

namespace DroneTrainer.Training.Core.Queries;

public sealed class TrainingSessionAttemptsQuery(int sessionId) : IRequest<IEnumerable<TrainingSessionAttempt>>
{
    public int SessionId { get; } = sessionId;
}
