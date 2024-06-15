using AutoMapper;
using DroneTrainer.Training.Telemetry.Inftrastructure.DTOs;
using DroneTrainer.Training.Telemetry.Worker.Mapping.Resolvers;
using DroneTrainer.Training.Telemetry.Worker.Models;

namespace DroneTrainer.Training.Telemetry.Worker.Mapping;

internal sealed class WorkerMapper : Profile
{
    public WorkerMapper()
    {
        CreateMap<SessionResultTelemetry, SessionResultTelemetryDTO>();
        CreateMap<StepPassTelemetry, StepPassTelemetryDTO>()
            .ForMember(dest => dest.PassedAt,
            opt => opt.MapFrom<StepPassDateTimeResolver, string>(src => src.PassedAt));
    }
}
