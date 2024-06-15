namespace DroneTrainer.Infrastructure.Settings;

public sealed class BackupCreateSettings
{
    public string CredentialIdentity { get; set; }
    public string CredentialSecret { get; set; }
    public string StorageUrl { get; set; }
    public string ContainerName { get; set; }
}
