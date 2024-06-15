using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Interfaces;
using System.Text.Json;

namespace DroneTrainer.Mobile.Services.Services;

internal class OrganizationService(IHttpClientFactory httpClientFactory) : IOrganizationService
{
    private readonly string _httpEndpointName = "organizations";
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.DroneTrainerHttpClient);

    public async Task<IEnumerable<OrganizationDevice>> GetOrganizationDevicesList(int organizationId)
    {
        var uri = string.Join('/', _httpEndpointName, organizationId, "devices");

        var response = await _httpClient.GetAsync(uri);

        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<IEnumerable<OrganizationDevice>>(responseJson);
    }
}
