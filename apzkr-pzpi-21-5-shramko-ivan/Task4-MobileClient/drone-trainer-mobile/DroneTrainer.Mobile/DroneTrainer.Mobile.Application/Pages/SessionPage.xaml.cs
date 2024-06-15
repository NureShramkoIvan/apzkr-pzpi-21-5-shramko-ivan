using DroneTrainer.Mobile.Core.Enums;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;

namespace DroneTrainer.Mobile.Application.Pages;

[QueryProperty(nameof(Session), "Session")]
public partial class SessionPage : ContentPage
{
    UserTrainingSession _session;
    private readonly IUserIdentityService _userIdentityService;
    private readonly ITrainingGroupService _trainingGroupService;
    private readonly IOrganizationService _organizationService;
    private readonly ITrainingSessionService _trainingSessionService;
    private readonly ITrainingProgramService _trainingProgramService;
    private readonly ITrainingSessionResultService _trainingSessionResultService;
    private bool _isSessionStarted = false;
    private bool _isActiveAttempt = false;
    private readonly List<Button> _attemptButtons = [];
    private readonly List<Guid> _finishedAttemptButtonsIds = [];
    private Button _finishSessionButton = null;
    private TrainingProgram _trainingProgram;
    private IEnumerable<TrainingGroupParticipation> _participations = Enumerable.Empty<TrainingGroupParticipation>();
    private IEnumerable<UserAttempt> _userAttempts = [];

    public LocalizationResourseManager LocalizationResourseManager
        => LocalizationResourseManager.Instance;

    public UserTrainingSession Session
    {
        get => _session; set
        {
            _session = value;
            LoadPage();

            Title = LocalizationResourseManager["SessionTitle"].ToString();
        }
    }

    public SessionPage(
        IUserIdentityService userIdentityService,
        ITrainingGroupService trainingGroupService,
        IOrganizationService organizationService,
        ITrainingSessionService trainingSessionService,
        ITrainingProgramService trainingProgramService,
        ITrainingSessionResultService trainingSessionResultService)
    {
        InitializeComponent();
        _userIdentityService = userIdentityService;
        _trainingGroupService = trainingGroupService;
        _organizationService = organizationService;
        _trainingSessionService = trainingSessionService;
        _trainingProgramService = trainingProgramService;
        _trainingSessionResultService = trainingSessionResultService;

        BindingContext = this;
        LocalizationResourseManager.LocaleChaged += OnLocaleChanged;
    }

    private void OnLocaleChanged(object sender, EventArgs args)
    {
        LoadPage();
    }

    private async void LoadPage()
    {
        _trainingProgram = await _trainingProgramService.GetTrainingProgram(Session.ProgramId);
        _participations = await _trainingGroupService.GetGroupParticipations(Session.GroupId);

        if (_userIdentityService.Role == Role.Instructor)
        {
            CreateInstructorView();
        }
        else
        {
            CreateUserView();
        }
    }

    public async void OnStartSessionButtonClicked(object sender, EventArgs eventArgs)
    {
        await _trainingSessionService.StartTrainingSessionAsync(
            Session.Id,
            _participations.Select(p => p.UserId),
            _trainingProgram.Steps.Select(s => s.DeviceUniqueId),
            "en-US");

        _userAttempts = await _trainingSessionService.GetUserAttemptIds(_participations.Select(p => p.UserId), Session.Id);

        _isSessionStarted = true;
        ((Button)sender).IsEnabled = false;
        _attemptButtons.ForEach(b => b.IsEnabled = true);
    }

    public async void OnEndSessionButtonClicked(object sender, EventArgs eventArgs)
    {
        await _trainingSessionService.EndTrainingSessionAsync(Session.Id, _trainingProgram.Steps.Select(s => s.DeviceUniqueId));
        _isSessionStarted = false;
        ((Button)sender).IsEnabled = false;
    }

    public void OnAttemptButtonClicked(object sender, EventArgs eventArgs)
    {
        var label = (Label)((HorizontalStackLayout)((Button)sender).Parent).Children.Where(c => c is Label).Single();

        var userId = int.Parse(label.Text);
        var attemptId = _userAttempts.Single(ua => ua.UserId == userId).AttemptId;

        if (_isActiveAttempt)
        {
            _trainingSessionService.EndTrainingSessionAttemptAsync(Session.Id, attemptId, _trainingProgram.Steps.Select(s => s.DeviceUniqueId));
        }
        else
        {
            _trainingSessionService.StartTrainingSessionAttemptAsync(Session.Id, attemptId, _trainingProgram.Steps.Select(s => s.DeviceUniqueId));
        }

        _isActiveAttempt = !_isActiveAttempt;
        ((Button)sender).Text = LocalizationResourseManager["SessionEndAttempt"].ToString();
        ((Button)sender).BackgroundColor = Color.FromArgb("#f76565");
        var clickedButtonId = ((Button)sender).Id;

        foreach (var button in _attemptButtons.Where(b => b.Id != clickedButtonId && !_finishedAttemptButtonsIds.Contains(b.Id)))
        {
            button.IsEnabled = !_isActiveAttempt;
        }

        if (!_isActiveAttempt)
        {
            _finishedAttemptButtonsIds.Add(clickedButtonId);
            ((Button)sender).IsEnabled = false;
        }

        if (_finishedAttemptButtonsIds.Count == _attemptButtons.Count)
        {
            _finishSessionButton.IsEnabled = true;
        }
    }

    private void CreateInstructorView()
    {
        var startSessionButton = new Button
        {
            Text = LocalizationResourseManager["SessionStartSession"].ToString()
        };
        startSessionButton.Clicked += OnStartSessionButtonClicked;

        var endSessionButton = new Button
        {
            Text = LocalizationResourseManager["SessionEndSession"].ToString(),
            BackgroundColor = Color.FromArgb("#f76565"),
            IsEnabled = false
        };
        endSessionButton.Clicked += OnEndSessionButtonClicked;

        _finishSessionButton = endSessionButton;

        var body = new ListView
        {
            ItemsSource = _participations,
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label();
                label.SetBinding(Label.TextProperty, "UserId");

                var button = new Button
                {
                    Text = LocalizationResourseManager["SessionStartAttempt"].ToString(),
                    IsEnabled = false
                };

                button.Clicked += OnAttemptButtonClicked;

                _attemptButtons.Add(button);

                return new ViewCell
                {
                    View = new HorizontalStackLayout
                    {
                        Children = { label, button }
                    }
                };
            })
        };

        Content = new VerticalStackLayout
        {
            Children = { startSessionButton, body, endSessionButton }
        };

    }
    private async void CreateUserView()
    {
        var trainingSessionResult = await _trainingSessionResultService.GetUserTrainingSessionResult(_userIdentityService.UserId.Value, Session.Id);

        var sucessFullGatesPercentTitle = new Label() { Text = LocalizationResourseManager["SessionSuccessfullGatesPercent"].ToString(), FontSize = 18, FontAttributes = FontAttributes.Bold };
        var unsucessFullGatesPercentTitle = new Label() { Text = LocalizationResourseManager["SessionUnsuccessfullGatesPercent"].ToString(), FontSize = 18, FontAttributes = FontAttributes.Bold };
        var sessionCompletionTimeTitle = new Label() { Text = LocalizationResourseManager["SessionSessionCompletionTime"].ToString(), FontSize = 18, FontAttributes = FontAttributes.Bold };
        var userSuccessCoeficientTitle = new Label() { Text = LocalizationResourseManager["SessionUserSuccessCoeficient"].ToString(), FontSize = 18, FontAttributes = FontAttributes.Bold };

        var sucessFullGatesPercentField = new Label() { Text = trainingSessionResult.SuccessfullGatesPercent.ToString("P") };
        var unsucessFullGatesPercentField = new Label() { Text = trainingSessionResult.UnsuccessfullGatesPercent.ToString("P") };
        var sessionCompletionTimeField = new Label() { Text = trainingSessionResult.SessionCompletionTime.ToString(@"mm\:ss") };
        var userSuccessCoeficientField = new Label() { Text = trainingSessionResult.UserSuccessCoefficient.ToString("F2") };

        Content = new VerticalStackLayout
        {
            Children =
            {
                sucessFullGatesPercentTitle,
                sucessFullGatesPercentField,
                unsucessFullGatesPercentTitle,
                unsucessFullGatesPercentField,
                sessionCompletionTimeTitle,
                sessionCompletionTimeField,
                userSuccessCoeficientTitle,
                userSuccessCoeficientField
            }
        };
    }
}