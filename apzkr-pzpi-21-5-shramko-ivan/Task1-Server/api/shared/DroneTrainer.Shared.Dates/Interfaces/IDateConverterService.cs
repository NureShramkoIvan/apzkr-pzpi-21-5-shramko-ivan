using DroneTrainer.Shared.Dates.Types.Enums;

namespace DroneTrainer.Shared.Dates.Services.Interfaces;

public interface IDateConverterService
{
    public DateTime ConvertToSupportedTimeZoneDateTime(DateTime utcDateTime, SupportedTimeZone timeZone);
    public TimeSpan GetSupportedTimeZoneOffset(SupportedTimeZone timeZone);
}
