using AutoMapper;
using DroneTrainer.Training.Telemetry.Worker.Models;

namespace DroneTrainer.Training.Telemetry.Worker.Mapping.Resolvers;
internal class StepPassDateTimeResolver
    : IMemberValueResolver<
        object,
        object,
        string,
        DateTime>
{
    public DateTime Resolve(
        object source,
        object destination,
        string sourceMember,
        DateTime destMember,
        ResolutionContext context)
    {
        var format = ((StepPassTelemetry)source).Locale == "en-US" ? "yyyy-MM-dd hh:mm:ss tt" : "yyyy-MM-dd HH:mm:ss";
        return DateTime.ParseExact(sourceMember, format, null);
    }
}
