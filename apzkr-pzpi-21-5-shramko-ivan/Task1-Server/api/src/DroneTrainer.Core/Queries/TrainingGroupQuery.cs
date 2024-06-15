using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class TrainingGroupQuery(int id) : IRequest<TrainingGroup>
{
    public int Id { get; } = id;
}
