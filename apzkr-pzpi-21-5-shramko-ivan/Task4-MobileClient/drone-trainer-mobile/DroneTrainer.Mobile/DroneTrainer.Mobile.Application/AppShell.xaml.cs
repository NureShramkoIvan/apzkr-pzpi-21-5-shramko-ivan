using DroneTrainer.Mobile.Services.Interfaces;
using System.Globalization;

namespace DroneTrainer.Mobile.Application;

public partial class AppShell : Shell
{
    private readonly IAuthService _authService;

    public string PageTitle { get; set; }

    public string[] Langs { get; set; } = { "en-US", "uk-UA" };

    public LocalizationResourseManager LocalizationResourseManager
        => LocalizationResourseManager.Instance;

    public AppShell(IAuthService authService)
    {
        InitializeComponent();
        BindingContext = this;
        _authService = authService;
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);
        UpdateTitleText();
        //OnPropertyChanged(nameof(PageTitle));
    }

    void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var langIndex = ((Picker)sender).SelectedIndex;
        LocalizationResourseManager.SetCulture(new CultureInfo(Langs[langIndex]));
        UpdateTitleText();
    }

    private void UpdateTitleText()
    {
        PageTitle = Current.CurrentItem.Route switch
        {
            "IMPL_home" => LocalizationResourseManager["HomeTitle"].ToString(),
            "IMPL_login" => LocalizationResourseManager["LoginTitle"].ToString(),
            "IMPL_session" => LocalizationResourseManager["SessionTitle"].ToString()
        };
        OnPropertyChanged(nameof(PageTitle));
    }

    private async void OnLogout(object sender, EventArgs args)
    {
        _authService.Logout();
        await Current.GoToAsync("//login");
    }
}
