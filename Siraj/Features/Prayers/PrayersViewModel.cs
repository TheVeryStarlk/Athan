using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Siraj.Features.Locations;
using Siraj.Features.Prayers.Calculation;
using Siraj.Features.Settings;
using Siraj.Features.Shell.Items;

namespace Siraj.Features.Prayers;

internal sealed partial class PrayersViewModel : HeaderViewModel
{
    public Location Location { get; }

    public ObservableCollection<Prayer> Prayers { get; } = [];

    [ObservableProperty]
    public partial string? Hijri { get; set; }

    [ObservableProperty]
    public partial string? Upcoming { get; set; }

    [ObservableProperty]
    public partial string? Message { get; set; }

    private readonly SettingsService settingsService;
    private readonly TimerService timerService;
    private readonly TimeProvider timeProvider;

    public PrayersViewModel(
        Location location,
        SettingsService settingsService,
        TimerService timerService,
        TimeProvider timeProvider)
    {
        Location = location;

        this.settingsService = settingsService;
        this.timerService = timerService;
        this.timeProvider = timeProvider;

        Title = location.Name;
        Glyph = "🌄";
        Deletable = true;
    }

    [RelayCommand]
    private void Initialize()
    {
        Prayers.Clear();

        var now = timeProvider.GetLocalNow();

        Hijri = now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        var options = settingsService.Get(PrayerCalculation.Makkah, SirajSerializerContext.Default.PrayerCalculation) switch
        {
            PrayerCalculation.Makkah => MakkahPrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.Egypt => EgyptPrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.Karachi => KarachiPrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.France => FrancePrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.Russia => RussiaPrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.Singapore => SingaporePrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.MuslimWorldLeague => MuslimWorldLeaguePrayerTimesCalculatorOptions.Instance,
            PrayerCalculation.IslamicSocietyOfNorthAmerica => IslamicSocietyOfNorthAmericaPrayerTimesCalculatorOptions.Instance,
            _ => throw new ArgumentOutOfRangeException()
        };

        var calculator = new PrayerTimesCalculator(options);
        var times = calculator.Calculate(now, Location.Latitude, Location.Longitude);

        foreach (var pair in times)
        {
            var local = pair.Value.ToLocalTime();
            Prayers.Add(new Prayer(pair.Key.ToString(), local.ToString("h:mm tt"), local));
        }

        Refresh();

        var elapsed = TimeSpan.FromTicks(now.TimeOfDay.Ticks % TimeSpan.FromMinutes(1).Ticks);
        var interval = elapsed == TimeSpan.Zero ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(1) - elapsed;

        timerService.Start(interval, Update);
    }

    private void Update()
    {
        Refresh();
        timerService.Start(TimeSpan.FromMinutes(1), Update);
    }

    private void Refresh()
    {
        var now = timeProvider.GetLocalNow();
        var upcoming = Prayers.FirstOrDefault(prayer => prayer.Time > now);

        // Show how much is left for Fajr in the next day.
        if (upcoming is null)
        {
            var prayer = Prayers[0];
            upcoming = new Prayer(prayer.Name, prayer.Message, prayer.Time.AddDays(1));
        }

        Glyph = upcoming.Name switch
        {
            "Fajr" => "🌅",
            "Dhuhr" => "🌄",
            "Asr" => "🌇",
            "Maghrib" => "🌆",
            "Isha" => "🌌",
            _ => throw new ArgumentOutOfRangeException()
        };

        Upcoming = upcoming.Name;

        var left = upcoming.Time - now;
        var hours = (int) left.TotalHours;
        var minutes = left.Minutes;

        Message = (hours, minutes) switch
        {
            (> 0, > 0) => $"{hours} hour(s) and {minutes} minute(s) left",
            (> 0, 0) => $"{hours} hour(s) left",
            (0, > 0) => $"{minutes} minute(s) left",
            (0, 0) => "Less than a minute left",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

internal sealed class Prayer(string name, string message, DateTimeOffset time)
{
    public string Name => name;

    public string Message => message;

    public DateTimeOffset Time => time;
}