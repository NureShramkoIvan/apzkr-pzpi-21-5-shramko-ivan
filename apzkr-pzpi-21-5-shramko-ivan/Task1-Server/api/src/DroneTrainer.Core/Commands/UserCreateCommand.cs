using DroneTrainer.Core.Enums;
using DroneTrainer.Shared.Dates.Types.Enums;
using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class UserCreateCommand(
    string userName,
    string normalizedUserName,
    string passwordHash,
    Role role,
    int organizationId,
    SupportedTimeZone userTimeZone) : IRequest
{
    public string UserName { get; } = userName;
    public string NormalizedUserName { get; } = normalizedUserName;
    public string PasswordHash { get; } = passwordHash;
    public Role Role { get; } = role;
    public int OrganizationId { get; } = organizationId;
    public SupportedTimeZone UserTimeZone { get; } = userTimeZone;
}
