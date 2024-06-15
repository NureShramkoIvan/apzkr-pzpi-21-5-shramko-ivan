using DroneTrainer.Core.Enums;
using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class OrganizationDeviceCreateCommand(DeviceType type, int organizationId) : IRequest
{
    public DeviceType Type { get; } = type;
    public int OrganizationId { get; } = organizationId;
}
