using System.Security.Claims;

namespace DroneTrainer.Core.Commands;

public sealed class UserClaimsCreateCommand(int userId, IEnumerable<Claim> claims)
{
    public int UserId { get; } = userId;
    public IEnumerable<Claim> Claims { get; } = claims;
}
