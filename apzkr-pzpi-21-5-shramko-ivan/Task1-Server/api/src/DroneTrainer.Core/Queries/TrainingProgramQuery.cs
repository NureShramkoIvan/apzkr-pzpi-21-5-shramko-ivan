using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class TrainingProgramQuery(int id) : IRequest<TrainingProgram>
{
    public int Id { get; } = id;
}
