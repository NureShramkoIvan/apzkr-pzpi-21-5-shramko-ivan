using DroneTrainer.Shared.Dates.Services.Interfaces;
using DroneTrainer.Shared.Dates.Services.Services;

namespace DroneTrainer.Shared.Dates.Services;

public static class DateConverterFactoty
{
    public static IDateConverterService CreateDateConverter() => new DateConverterService();
}
