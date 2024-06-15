using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Application;

public partial class App : IApplication
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        MainPage = new AppShell(serviceProvider.GetService<IAuthService>());

        Shell.Current.GoToAsync("//login");
    }
}
