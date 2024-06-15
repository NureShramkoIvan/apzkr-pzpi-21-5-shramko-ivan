using DroneTrainer.Core.Models;
using MediatR;

namespace DroneTrainer.Core.Queries;

public sealed class UserByUserNameQuery(string normalizedUserName) : IRequest<User>
{
    public string NormalizedUserName { get; } = normalizedUserName;
}
