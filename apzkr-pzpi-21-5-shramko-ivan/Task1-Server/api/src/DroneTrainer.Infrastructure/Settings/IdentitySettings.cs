namespace DroneTrainer.Infrastructure.Settings;

public sealed class IdentitySettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public int TokenExpiresAfterSeconds { get; set; }
}