namespace DroneTrainer.Mobile.Services.Interfaces;

internal interface ICredentialsService
{
    public string AccessToken { get; }
    public string UserName { get; }
    public string Password { get; }

    void SaveAccesTokne(string token);
    void SaveCredentials(string userName, string password);
}
