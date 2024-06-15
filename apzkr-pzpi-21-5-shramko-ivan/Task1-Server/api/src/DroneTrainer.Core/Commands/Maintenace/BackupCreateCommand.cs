using MediatR;

namespace DroneTrainer.Core.Commands.Maintenace;

public sealed class BackupCreateCommand(
    string storageUrl,
    string credentialIdentity,
    string credentialSecret) : IRequest<string>
{
    public string StorageUrl { get; } = storageUrl;
    public string CredentialIdentity { get; } = credentialIdentity;
    public string CredentialSecret { get; } = credentialSecret;
}
