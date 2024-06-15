using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class OrganizationDevicesQuery(int organizationId) : IRequest<IEnumerable<OrganizationDevice>>
{
    public int OrganizationId { get; } = organizationId;
}
