using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class TrainingGroupNameCheckQuery(string name, int organizationId) : IRequest<bool>
{
    public string Name { get; } = name;
    public int OrganizationId { get; } = organizationId;
}
