using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class TrainingProgramCreateCommand(int organizationId, IEnumerable<TrainingProgramStep> steps) : IRequest
{
    public int OrganizationId { get; } = organizationId;
    public IEnumerable<TrainingProgramStep> Steps { get; } = steps;
}
