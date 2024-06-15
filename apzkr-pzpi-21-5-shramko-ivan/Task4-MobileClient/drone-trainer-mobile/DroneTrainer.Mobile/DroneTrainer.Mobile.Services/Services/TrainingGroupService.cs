using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;
using System.Text.Json;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class TrainingGroupService(IHttpClientFactory httpClientFactory) : ITrainingGroupService
{
    private readonly string _httpEndpointName = "training-groups";
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.DroneTrainerHttpClient);

    public async Task<IEnumerable<TrainingGroupParticipation>> GetGroupParticipations(int groupId)
    {
        var uri = string.Join('/', _httpEndpointName, groupId, "participations");

        var response = await _httpClient.GetAsync(uri);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<TrainingGroupParticipation>>(responseJson);
    }
}
