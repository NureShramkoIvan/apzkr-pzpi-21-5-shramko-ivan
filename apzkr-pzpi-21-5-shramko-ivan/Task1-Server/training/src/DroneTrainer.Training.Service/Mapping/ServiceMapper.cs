using AutoMapper;
using DroneTrainer.Training.Infrastructure.DTOS;
using DroneTrainer.Training.Service.Mapping.Resolvers;

namespace DroneTrainer.Training.Service.Mapping;

public sealed class ServiceMapper : Profile
{
    public ServiceMapper()
    {
        CreateMap<UserTrainingSessionResultDTO, UserTrainingResultResponse>()
            .ForMember(
                dest => dest.SessionCompletionTime,
                opt => opt.MapFrom<TimeSpanToDurationResolver, TimeSpan>(src => src.SessionCompletionTime));
    }
}
