using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace DroneTrainer.Training.Service.Mapping.Resolvers;

public sealed class TimeSpanToDurationResolver
    : IMemberValueResolver<
        object,
        object,
        TimeSpan,
        Duration>
{
    public Duration Resolve(object source, object destination, TimeSpan sourceMember, Duration destMember, ResolutionContext context)
        => Duration.FromTimeSpan(sourceMember);
}
