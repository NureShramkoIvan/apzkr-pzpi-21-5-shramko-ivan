using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;
using System.Text.Json;
using static DroneTrainer.Mobile.Services.TrainingSession;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class TrainingSessionService : ITrainingSessionService
{
    private readonly TrainingSessionClient _trainingSessionClient;
    private readonly ICredentialsService _credentialsService;
    private readonly HttpClient _httpClient;
    private readonly string _httpEndpointName = "users";

    public TrainingSessionService(
        TrainingSessionClient trainingSessionClient,
        IHttpClientFactory httpClientFactory,
        ICredentialsService credentialsService)
    {
        _trainingSessionClient = trainingSessionClient;
        _credentialsService = credentialsService;
        _httpClient = httpClientFactory.CreateClient(HttpClientNames.DroneTrainerHttpClient);
    }

    public async Task EndTrainingSessionAsync(int sessionId, IEnumerable<string> deviceUniqueIds)
    {
        var request = new EndTrainingSessionRequest
        {
            SessionId = sessionId
        };

        request.DeviceIds.AddRange(deviceUniqueIds);

        await _trainingSessionClient.EndTrainingSessionAsync(request);
    }

    public async Task EndTrainingSessionAttemptAsync(
        int sessionId,
        int attemptId,
        IEnumerable<string> deviceUniqueIds)
    {
        var request = new EndTrainingSessionAttemptRequest
        {
            AttemptId = attemptId,
            SesisonId = sessionId
        };

        request.DeviceIds.AddRange(deviceUniqueIds);

        await _trainingSessionClient.EndTrainingSessionAttemptAsync(request);
    }

    public async Task<IEnumerable<Core.Models.UserAttempt>> GetUserAttemptIds(IEnumerable<int> userIds, int sessionId)
    {
        var request = new GetUserAttemptIdsRequest();
        request.UserIds.Add(userIds);
        request.SessionId = sessionId;

        var response = await _trainingSessionClient.GetUserAttemptIdsAsync(request);

        return response.UserAttempts.Select(ua => new Core.Models.UserAttempt { AttemptId = ua.AttemptId, UserId = ua.UserId });
    }

    public async Task<IEnumerable<UserTrainingSession>> GetUserTrainingSessions(int userId)
    {
        var uri = string.Join('/', _httpEndpointName, userId, "training-sessions");

        var response = await _httpClient.GetAsync(uri);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<UserTrainingSession>>(responseJson);
    }

    public async Task StartTrainingSessionAsync(
        int sessionId,
        IEnumerable<int> userIds,
        IEnumerable<string> deviceUniqueIds,
        string locale)
    {
        var request = new StartTrainingSessionRequest
        {
            SessionId = sessionId,
            Locale = locale
        };

        request.UserIds.AddRange(userIds);
        request.DeviceIds.AddRange(deviceUniqueIds);

        await _trainingSessionClient.StartTrainignSessionAsync(request);
    }

    public async Task StartTrainingSessionAttemptAsync(
        int sessionId,
        int attemptId,
        IEnumerable<string> deviceUniqueIds)
    {
        var request = new StartTrainingSessionAttemptRequest
        {
            SessionId = sessionId,
            AttemptId = attemptId,
        };

        request.DeviceIds.AddRange(deviceUniqueIds);

        await _trainingSessionClient.StartTrainingSesisonAttemptAsync(request);
    }
}
