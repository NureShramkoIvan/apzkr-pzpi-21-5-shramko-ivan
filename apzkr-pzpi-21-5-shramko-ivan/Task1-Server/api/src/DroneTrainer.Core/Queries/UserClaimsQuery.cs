using MediatR;
using System.Security.Claims;

namespace DroneTrainer.Core.Queries;

public sealed class UserClaimsQuery(int userId) : IRequest<IEnumerable<Claim>>
{
    public int UserId { get; } = userId;
}
