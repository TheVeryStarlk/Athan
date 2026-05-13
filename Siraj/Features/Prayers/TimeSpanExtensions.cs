using System;

namespace Siraj.Features.Prayers;

internal static class TimeSpanExtensions
{
    public static string ToReadable(this TimeSpan timeSpan)
    {
        var hours = timeSpan.Hours;
        var hoursSuffix = hours > 1 ? "hours" : "hour";

        var minutes = timeSpan.Minutes;
        var minutesSuffix = minutes > 1 ? "minutes" : "minute";
        
        return (hours, minutes) switch
        {
            (> 0, > 0) => $"{hours} {hoursSuffix} and {minutes} {minutesSuffix} left",
            (> 0, 0) => $"{hours} {hoursSuffix} left",
            (0, > 0) => $"{minutes} {minutesSuffix} left",
            (0, 0) => "Less than a minute left",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}