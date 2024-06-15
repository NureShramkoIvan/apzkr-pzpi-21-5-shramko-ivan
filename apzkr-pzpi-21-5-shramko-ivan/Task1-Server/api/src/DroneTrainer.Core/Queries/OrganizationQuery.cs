using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class OrganizationQuery(int id) : IRequest<Organization>
{
    public int Id { get; } = id;
}
