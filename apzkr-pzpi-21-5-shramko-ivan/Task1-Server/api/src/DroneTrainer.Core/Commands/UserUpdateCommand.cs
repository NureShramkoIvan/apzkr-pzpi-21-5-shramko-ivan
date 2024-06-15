using MediatR;

namespace DroneTrainer.Core.Commands;

public sealed class UserUpdateCommand(
    int id,
    string userName,
    string normalizedUserName,
    string passwordHash) : IRequest
{
    public int Id { get; } = id;
    public string UserName { get; } = userName;
    public string NormalizedUserName { get; } = normalizedUserName;
    public string PasswordHash { get; } = passwordHash;
}
