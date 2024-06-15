using DroneTrainer.Mobile.Core.Constants;
using DroneTrainer.Mobile.Core.Exeptions;
using DroneTrainer.Mobile.Services.HttpMessageHandlers;
using DroneTrainer.Mobile.Services.Interfaces;
using DroneTrainer.Mobile.Services.Services;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Reflection;

namespace DroneTrainer.Mobile.Services;

public static class DependencyModule
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        var channel = GrpcChannel.ForAddress("http://10.0.2.2:5010");

        services.AddScoped(sp => new TrainingResult.TrainingResultClient(channel));
        services.AddScoped(sp => new TrainingSession.TrainingSessionClient(channel));

        services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyModule))!);

        services.AddHttpClient(HttpClientNames.DroneTrainerHttpClient, client =>
        {
            client.BaseAddress = new Uri("http://10.0.2.2:64608/api/");
        }).AddPolicyHandler((provider, _) => GetExpiredTokenRetryPolicy(provider))
        .AddHttpMessageHandler<ExpiredAccessTokenHandler>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<ITrainingGroupService, TrainingGroupService>();
        services.AddScoped<ITrainingSessionResultService, TrainingSessionResultService>();
        services.AddScoped<ITrainingSessionService, TrainingSessionService>();
        services.AddSingleton<IUserIdentityService, UserIdentityService>();
        services.AddSingleton<ICredentialsService, CredentialsService>();
        services.AddScoped<ITrainingProgramService, TrainingProgramService>();
        services.AddTransient<ExpiredAccessTokenHandler>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetExpiredTokenRetryPolicy(IServiceProvider serviceProvider)
    {
        return Policy<HttpResponseMessage>
            .Handle<OutdatedAcessTokenExeption>()
            .RetryAsync(async (_, _) => await serviceProvider.GetRequiredService<IAuthService>().RefreshAccessToken());
    }
}
