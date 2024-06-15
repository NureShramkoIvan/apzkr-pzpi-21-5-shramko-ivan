using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Enums;
using DroneTrainer.Mobile.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DroneTrainer.Mobile.Services.Services;

internal sealed class AuthService(
    IHttpClientFactory httpClientFactory,
    IUserIdentityService userIdentityService,
    ICredentialsService credentialsService) : IAuthService
{
    private readonly string _httpEndpointName = "token";
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(HttpClientNames.DroneTrainerHttpClient);
    private readonly IUserIdentityService _userIdentityService = userIdentityService;
    private readonly ICredentialsService _credentialsService = credentialsService;

    public async Task Authorize(string userName, string password)
    {
        var uri = string.Join('/', _httpEndpointName);

        var response = await _httpClient.PostAsync(uri, JsonContent.Create(new { UserName = userName, Password = password }));

        var responseJson = await response.Content.ReadAsStringAsync();

        var authorizeResult = JsonSerializer.Deserialize<AccessTokenVM>(responseJson);

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(authorizeResult.AccessToken);

        var userId = int.Parse(jwtToken.Claims.SingleOrDefault(c => c.Type == "user_id")?.Value);
        var organizationId = int.Parse(jwtToken.Claims.SingleOrDefault(c => c.Type == "organization_id")?.Value);
        var role = (Role)int.Parse(jwtToken.Claims.SingleOrDefault(c => c.Type == "role_id")?.Value);

        _userIdentityService.SetUserCliams(role, organizationId, userId);
        _credentialsService.SaveCredentials(userName, password);
        _credentialsService.SaveAccesTokne(authorizeResult.AccessToken);
    }

    public bool IsLoggedIn() => _userIdentityService.IsLoggedIn();

    public void Logout() => _userIdentityService.Logout();

    public async Task RefreshAccessToken()
    {
        await Authorize(_credentialsService.UserName, _credentialsService.Password);
    }

    private class AccessTokenVM
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }
    }
}
