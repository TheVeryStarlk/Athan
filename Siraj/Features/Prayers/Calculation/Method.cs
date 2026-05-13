using System;

namespace Siraj.Features.Prayers.Calculation;

internal enum Method
{
    Makkah,
    Egypt,
    Karachi,
    France,
    Russia,
    Singapore,
    MuslimWorldLeague,
    IslamicSocietyOfNorthAmerica
}

internal static class MethodExtensions
{
    public static PrayerTimesCalculatorOptions ToOptions(this Method method)
    {
        return method switch
        {
            Method.Makkah => MakkahPrayerTimesCalculatorOptions.Instance,
            Method.Egypt => EgyptPrayerTimesCalculatorOptions.Instance,
            Method.Karachi => KarachiPrayerTimesCalculatorOptions.Instance,
            Method.France => FrancePrayerTimesCalculatorOptions.Instance,
            Method.Russia => RussiaPrayerTimesCalculatorOptions.Instance,
            Method.Singapore => SingaporePrayerTimesCalculatorOptions.Instance,
            Method.MuslimWorldLeague => MuslimWorldLeaguePrayerTimesCalculatorOptions.Instance,
            Method.IslamicSocietyOfNorthAmerica => IslamicSocietyOfNorthAmericaPrayerTimesCalculatorOptions.Instance,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}