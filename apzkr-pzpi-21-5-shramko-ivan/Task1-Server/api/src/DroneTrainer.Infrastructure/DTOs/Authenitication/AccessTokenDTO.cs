namespace DroneTrainer.Infrastructure.DTOs.Authenitication;

public sealed class AccessTokenDTO
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}
