using DroneTrainer.Mobile.Core.Exeptions;
using DroneTrainer.Mobile.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace DroneTrainer.Mobile.Services.HttpMessageHandlers;

internal sealed class ExpiredAccessTokenHandler(ICredentialsService credentialsService) : DelegatingHandler
{
    private readonly ICredentialsService _credentialsService = credentialsService;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = _credentialsService.AccessToken;

        if (accessToken is not null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new OutdatedAcessTokenExeption();
        }

        return response;
    }
}
