using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;
using System.Text.Json;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class TrainingProgramService(IHttpClientFactory httpClientFactory) : ITrainingProgramService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.DroneTrainerHttpClient);
    private readonly string _endpointName = "training-programs";

    public async Task<TrainingProgram> GetTrainingProgram(int id)
    {
        var uri = string.Join('/', _endpointName, id);

        var response = await _httpClient.GetAsync(uri);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TrainingProgram>(responseJson);
    }
}
