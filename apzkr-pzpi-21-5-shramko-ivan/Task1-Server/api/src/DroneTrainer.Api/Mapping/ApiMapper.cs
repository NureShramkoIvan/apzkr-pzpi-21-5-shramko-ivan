using AutoMapper;
using DroneTrainer.Api.Mapping.Resolvers;
using DroneTrainer.Api.ViewModels;
using DroneTrainer.Api.ViewModels.Authentication;
using DroneTrainer.Api.ViewModels.Maintenance;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.DTOs.Authenitication;
using DroneTrainer.Infrastructure.DTOs.Maintenace;

namespace DroneTrainer.Api.Mapping;

internal sealed class ApiMapper : Profile
{
    public ApiMapper()
    {
        CreateMaintenanceMaps();
        CreateAuthenticationMaps();
        CreateMap<OrganizationCreateVM, OrganizationCreateDTO>();
        CreateMap<TrainingGroupCreateVM, TrainingGroupCreateDTO>();
        CreateMap<OrganizationDeviceCreateVM, OrganizationDeviceCreateDTO>();
        CreateMap<OrganizationDeviceDTO, OrganizationDeviceVM>();
        CreateMap<TrainingProgramCreateVM, TrainingProgramCreateDTO>();
        CreateMap<TrainingProgramStepVM, TrainingProgramStepDTO>();
        CreateMap<TrainingSessionCreateVM, TrainingSessionCreateDTO>()
            .ForMember(dest => dest.ScheduledAt, opt => opt.MapFrom<DateTimeOffsetToUTCResolver, DateTimeOffset>(src => src.ScheduledAt));

        CreateMap<TrainingGroupParticipationCreateVM, TrainingGroupParticipationCreateDTO>();
        CreateMap<TrainingSessionDTO, TrainingSessionVM>();
        CreateMap<UserTrainingSessionResultDTO, UserTrainingSessionResultVM>();
    }

    private void CreateMaintenanceMaps()
    {
        CreateMap<BackupDTO, BackupVM>();
    }

    private void CreateAuthenticationMaps()
    {
        CreateMap<UserRegisterVM, UserRegisterDTO>();
        CreateMap<AccessTokenDTO, AccessTokenVM>();
    }
}
