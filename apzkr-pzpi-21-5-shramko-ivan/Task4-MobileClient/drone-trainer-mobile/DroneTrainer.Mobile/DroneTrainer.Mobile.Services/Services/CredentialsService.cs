using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class CredentialsService : ICredentialsService
{
    public string AccessToken { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public void SaveAccesTokne(string token)
    {
        AccessToken = token;
    }

    public void SaveCredentials(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}
