using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class OrganizationNameCheckQuery(string name) : IRequest<bool>
{
    public string Name { get; } = name;
}
