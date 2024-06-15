using AutoMapper;
using DroneTrainer.Mobile.Core.Models;
using DroneTrainer.Mobile.Services.Mapping.Resolvers;
using Google.Protobuf.WellKnownTypes;

namespace DroneTrainer.Mobile.Services.Mapping;

internal class DataAccessMapper : Profile
{
    public DataAccessMapper()
    {
        CreateMap<UserTrainingResultResponse, UserTrainingSessionResult>()
            .ForMember(dest => dest.SessionCompletionTime,
            opt => opt.MapFrom<DurationToTimeSpanResolver, Duration>(src => src.SessionCompletionTime));
    }
}
