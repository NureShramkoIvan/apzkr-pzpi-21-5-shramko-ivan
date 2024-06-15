using AutoMapper;
using DroneTrainer.Core.Models;
using DroneTrainer.Infrastructure.DTOs;
using DroneTrainer.Infrastructure.DTOs.Maintenace;
using DroneTrainer.Infrastructure.Mapping.Resolvers;
using DroneTrainer.Training.Service;
using Google.Protobuf.WellKnownTypes;

namespace DroneTrainer.Infrastructure.Mapping;

internal sealed class InfrastructureMapper : Profile
{
    public InfrastructureMapper()
    {
        CreateMap<Backup, BackupDTO>();
        CreateMap<OrganizationDevice, OrganizationDeviceDTO>();
        CreateMap<TrainingProgramStepDTO, TrainingProgramStep>();
        CreateMap<TrainingSession, TrainingSessionDTO>();
        CreateMap<UserTrainingResultResponse, UserTrainingSessionResultDTO>()
            .ForMember(dest => dest.SessionCompletionTime,
            opt => opt.MapFrom<DurationToTimeSpanResolver, Duration>(src => src.SessionCompletionTime));
    }
}
