using DroneTrainer.Training.Core.Models;
using MediatR;

namespace DroneTrainer.Training.Core.Queries;

public sealed class TrainingSessionAttemptStepsQuery(int sessionId) : IRequest<IEnumerable<TrainingSessionAttemptStep>>
{
    public int SessionId { get; } = sessionId;
}
