using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class OrganizationCreateCommand(string name) : IRequest
{
    public string Name { get; } = name;
}
