using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class UserByIdQuery(int id) : IRequest<User>
{
    public int Id { get; } = id;
}
