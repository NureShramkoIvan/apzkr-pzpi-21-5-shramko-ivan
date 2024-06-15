using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Application.Pages;

public partial class LoginPage : ContentPage
{
    private string _userName;
    private string _password;
    private readonly IAuthService _authService;

    public LocalizationResourseManager LocalizationResourseManager
        => LocalizationResourseManager.Instance;

    public LoginPage(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;

        Title = LocalizationResourseManager["LoginTitle"].ToString();

        if (_authService.IsLoggedIn())
        {
            Shell.Current.GoToAsync("//home");
        }

        BindingContext = this;
    }

    public void OnUserNameEntered(object sender, TextChangedEventArgs eventArgs) => _userName = eventArgs.NewTextValue;

    public void OnPasswordEntered(object sender, TextChangedEventArgs eventArgs) => _password = eventArgs.NewTextValue;

    public async void OnLogin(object sender, EventArgs eventArgs)
    {
        await _authService.Authorize(_userName, _password);

        if (_authService.IsLoggedIn())
        {
            await Shell.Current.GoToAsync("//home");
        }
    }
}