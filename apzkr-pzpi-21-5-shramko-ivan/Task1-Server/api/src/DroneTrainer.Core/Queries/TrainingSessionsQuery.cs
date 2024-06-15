using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class TrainingSessionsQuery(int? userId) : IRequest<IEnumerable<TrainingSession>>
{
    public int? UserId { get; } = userId;
}
