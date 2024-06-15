namespace DroneTrainer.Mobile.Services.Interfaces;

public interface IAuthService
{
    Task Authorize(string userName, string password);
    Task RefreshAccessToken();
    bool IsLoggedIn();
    void Logout();
}
