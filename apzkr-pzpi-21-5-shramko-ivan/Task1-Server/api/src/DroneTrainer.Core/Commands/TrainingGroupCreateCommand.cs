using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class TrainingGroupCreateCommand(string name, int organizationId) : IRequest
{
    public string Name { get; } = name;
    public int OrganizationId { get; } = organizationId;
}
