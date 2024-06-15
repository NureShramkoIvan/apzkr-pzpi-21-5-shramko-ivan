using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Application.Pages;

public partial class HomePage : ContentPage
{
    private readonly ITrainingSessionService _trainingSessionService;
    private readonly IUserIdentityService _userIdentityService;

    public IEnumerable<UserTrainingSession> Sessions { get; set; }

    public LocalizationResourseManager LocalizationResourseManager
        => LocalizationResourseManager.Instance;

    public HomePage(ITrainingSessionService trainingSessionService, IUserIdentityService userIdentityService)
    {
        InitializeComponent();
        _trainingSessionService = trainingSessionService;
        _userIdentityService = userIdentityService;

        Title = LocalizationResourseManager["HomeTitle"].ToString();
        BindingContext = this;
        LocalizationResourseManager.LocaleChaged += OnLocaleChanged;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await LoadData();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }

    public async void OnRefresh(object sender, EventArgs eventArgs)
    {
        await LoadData();
    }

    public async void OnLocaleChanged(object sender, EventArgs eventArgs)
    {
        await LoadData();
    }

    public async void OnSessionSelected(object sender, EventArgs eventArgs)
    {
        var selectedSession = (sender as ListView).SelectedItem as UserTrainingSession;

        var navigationParameters = new ShellNavigationQueryParameters
        {
            { "Session", selectedSession }
        };

        await Shell.Current.GoToAsync($"//session", navigationParameters);
    }

    private async Task LoadData()
    {
        var sessions = await _trainingSessionService.GetUserTrainingSessions(_userIdentityService.UserId.Value);

        var listview = new ListView
        {
            ItemsSource = sessions,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, "ScheduledAt");

                return new ViewCell()
                {
                    View = new StackLayout
                    {
                        Children = { label }
                    }
                };
            })
        };

        listview.ItemSelected += OnSessionSelected;

        var refreshButton = new Button() { Text = LocalizationResourseManager["HomeRefreshButton"].ToString() };

        refreshButton.Clicked += OnRefresh;

        var layout = new VerticalStackLayout()
        {
            Children = { refreshButton, listview }
        };

        Content = new ScrollView
        {
            Content = layout
        };
    }
}