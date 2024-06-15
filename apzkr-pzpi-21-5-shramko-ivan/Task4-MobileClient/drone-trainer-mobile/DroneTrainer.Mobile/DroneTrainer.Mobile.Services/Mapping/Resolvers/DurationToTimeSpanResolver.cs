using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace DroneTrainer.Mobile.Services.Mapping.Resolvers;

public sealed class DurationToTimeSpanResolver
    : IMemberValueResolver<
        object,
        object,
        Duration,
        TimeSpan>
{
    public TimeSpan Resolve(
        object source,
        object destination,
        Duration sourceMember,
        TimeSpan destMember,
        ResolutionContext context) => sourceMember.ToTimeSpan();
}
