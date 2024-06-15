using DroneTrainer.Shared.Dates.Services.Interfaces;
using DroneTrainer.Shared.Dates.Types.Enums;

namespace DroneTrainer.Shared.Dates.Services.Services;

internal sealed class DateConverterService : IDateConverterService
{
    public DateTime ConvertToSupportedTimeZoneDateTime(DateTime utcDateTime, SupportedTimeZone timeZone)
    {
        return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDateTime, TimeZoneHelper.ToTimeZoneId(timeZone));
    }

    public TimeSpan GetSupportedTimeZoneOffset(SupportedTimeZone timeZone)
    {
        return new TimeSpan(TimeZoneHelper.ToTimeZoneOffset(timeZone), 0, 0);
    }
}
