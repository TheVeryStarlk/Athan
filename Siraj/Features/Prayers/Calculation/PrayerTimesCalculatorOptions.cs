using System;

namespace Siraj.Features.Prayers.Calculation;

internal sealed class PrayerTimesCalculatorOptions
{
    public double FajrAngle { get; init; }

    public double IshaAngle { get; init; }

    public TimeSpan? IshaOffset { get; init; }

    public TimeSpan? MaghribOffset { get; init; }
}

internal static class MakkahPrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 18.5,
        IshaOffset = TimeSpan.FromMinutes(90)
    };
}

internal static class EgyptPrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 19.5,
        IshaAngle = 17.5
    };
}

internal static class KarachiPrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 18,
        IshaAngle = 18
    };
}

internal static class FrancePrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 12,
        IshaAngle = 12
    };
}

internal static class RussiaPrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 16,
        IshaAngle = 15
    };
}

internal static class SingaporePrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 20,
        IshaAngle = 18
    };
}

internal static class MuslimWorldLeaguePrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 18,
        IshaAngle = 17
    };
}

internal static class IslamicSocietyOfNorthAmericaPrayerTimesCalculatorOptions
{
    public static PrayerTimesCalculatorOptions Instance { get; } = new()
    {
        FajrAngle = 15,
        IshaAngle = 15
    };
}