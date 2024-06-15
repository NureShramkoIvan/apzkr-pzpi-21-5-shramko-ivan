using DroneTrainer.Shared.Dates.Services.Constants;
using DroneTrainer.Shared.Dates.Types.Enums;

namespace DroneTrainer.Shared.Dates.Services.Services;

internal static class TimeZoneHelper
{
    public static string ToTimeZoneId(SupportedTimeZone supportedTimeZone)
        => supportedTimeZone switch
        {
            SupportedTimeZone.CentralStandardTime => SupportedTimeZoneIds.CentralStandardTime,
            SupportedTimeZone.TokyoStandardTime => SupportedTimeZoneIds.TokyoStandardTime,
            SupportedTimeZone.FLEStandardTime => SupportedTimeZoneIds.FLEStandardTime,
            _ => throw new NotImplementedException()
        };

    public static SupportedTimeZone? ToSupportedTimeZone(string zoneId)
        => zoneId switch
        {
            SupportedTimeZoneIds.CentralStandardTime => SupportedTimeZone.CentralStandardTime,
            SupportedTimeZoneIds.FLEStandardTime => SupportedTimeZone.FLEStandardTime,
            SupportedTimeZoneIds.TokyoStandardTime => SupportedTimeZone.TokyoStandardTime,
            _ => null
        };

    public static int ToTimeZoneOffset(SupportedTimeZone supportedTimeZone)
        => supportedTimeZone switch
        {
            SupportedTimeZone.CentralStandardTime => SupportedTimeZoneOffests.CentralStandardTime,
            SupportedTimeZone.TokyoStandardTime => SupportedTimeZoneOffests.TokyoStandardTime,
            SupportedTimeZone.FLEStandardTime => SupportedTimeZoneOffests.FLEStandardTime,
            _ => throw new NotImplementedException()
        };
}
