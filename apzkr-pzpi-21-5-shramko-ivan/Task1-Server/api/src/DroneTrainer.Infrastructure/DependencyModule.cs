using Azure.Storage.Blobs;
using DroneTrainer.Core.Models;
using DroneTrainer.DataAccess.SqlServer;
using DroneTrainer.Infrastructure.ApplicationServices;
using DroneTrainer.Infrastructure.InfrastructureServices;
using DroneTrainer.Infrastructure.Interfaces;
using DroneTrainer.Infrastructure.Settings;
using DroneTrainer.Shared.Dates.Services;
using DroneTrainer.Training.Service;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DroneTrainer.Infrastructure;

public static class DependencyModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string trainerDbConnectionString,
        IConfigurationSection identitySettings,
        IConfigurationSection backupCreateSettings,
        IConfigurationSection backupReadSettings)
    {
        services.Configure<IdentitySettings>(identitySettings);
        services.Configure<BackupCreateSettings>(backupCreateSettings);
        services.Configure<BackupReadSettings>(backupReadSettings);

        services.AddDataAccess(trainerDbConnectionString);

        services.AddAutoMapper(Assembly.GetAssembly(typeof(DependencyModule))!);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<ITraingGroupService, TrainingGroupService>();
        services.AddScoped<ITrainingProgramService, TrainingProgramService>();
        services.AddScoped<ITrainingSessionService, TrainingSessionService>();
        services.AddScoped<IBackupService, BackupService>();

        services.AddIdentityCore<User>();

        services.AddTransient<IUserStore<User>, UserStore>();
        services.AddTransient<IPasswordHasher<User>, UserPasswordHasher>();

        services.AddSingleton(sp => DateConverterFactoty.CreateDateConverter());

        services.AddScoped(sp => new BlobServiceClient(backupReadSettings["ConnectionString"]));

        services.AddScoped(sp =>
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5010");
            return new TrainingResult.TrainingResultClient(channel);
        });

        return services;
    }
}
