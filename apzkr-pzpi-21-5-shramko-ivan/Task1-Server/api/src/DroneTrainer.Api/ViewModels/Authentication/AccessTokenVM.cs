namespace DroneTrainer.Api.ViewModels.Authentication;

internal class AccessTokenVM
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
}
