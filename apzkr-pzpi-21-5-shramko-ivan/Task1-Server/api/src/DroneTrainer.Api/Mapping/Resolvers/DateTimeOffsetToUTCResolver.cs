using AutoMapper;

namespace DroneTrainer.Api.Mapping.Resolvers;

public sealed class DateTimeOffsetToUTCResolver
    : IMemberValueResolver<
        object,
        object,
        DateTimeOffset,
        DateTime>
{
    public DateTime Resolve(
        object source,
        object destination,
        DateTimeOffset sourceMember,
        DateTime destMember,
        ResolutionContext context) => sourceMember.UtcDateTime;
}
