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
    public override bool Deletable => true;

    public Location Location { get; }

    public ObservableCollection<Prayer> Prayers { get; } = [];

    [ObservableProperty]
    public partial string? Hijri { get; set; }

    [ObservableProperty]
    public partial string? Upcoming { get; set; }

    [ObservableProperty]
    public partial string? Remaining { get; set; }

    private readonly SettingsService settingsService;
    private readonly TimerService timerService;
    private readonly TimeProvider timeProvider;

    public PrayersViewModel(
        SettingsService settingsService,
        TimerService timerService,
        TimeProvider timeProvider,
        Location location)
    {
        this.settingsService = settingsService;
        this.timerService = timerService;
        this.timeProvider = timeProvider;

        Location = location;

        Title = location.Name;
    }

    [RelayCommand]
    private void Initialize()
    {
        Prayers.Clear();

        var now = timeProvider.GetLocalNow();

        Hijri = now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        var calculator = new PrayerTimesCalculator(settingsService.Method.ToOptions());
        var times = calculator.Calculate(now, Location.Latitude, Location.Longitude);

        foreach (var pair in times)
        {
            var local = pair.Value.ToLocalTime();
            Prayers.Add(new Prayer(pair.Key, local.ToString("h:mm tt"), local));
        }

        var elapsed = TimeSpan.FromTicks(now.TimeOfDay.Ticks % TimeSpan.FromMinutes(1).Ticks);
        var interval = elapsed == TimeSpan.Zero ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(1) - elapsed;

        timerService.Start(TimeSpan.Zero, Update);
    }


    private void Refresh()
    {
        var now = timeProvider.GetLocalNow();
        var upcoming = Prayers.FirstOrDefault(prayer => prayer.Time > now);

        // Show how much is left for Fajr in the next day.
        if (upcoming is null)
        {
            var prayer = Prayers[0];
            upcoming = new Prayer(prayer.Kind, prayer.Message, prayer.Time.AddDays(1));
        }

        Glyph = Prayer.ToEmoji(upcoming.Kind);

        Upcoming = upcoming.Kind.ToString();
        Remaining = (upcoming.Time - now).ToReadable();
    }
    
    private void Update()
    {
        Refresh();
        timerService.Start(TimeSpan.FromMinutes(1), Update);
    }
}

internal sealed class Prayer(PrayerKind kind, string message, DateTimeOffset time)
{
    public PrayerKind Kind => kind;

    public string Message => message;

    public DateTimeOffset Time => time;

    public static string ToEmoji(PrayerKind kind)
    {
        return kind switch
        {
            PrayerKind.Fajr => "🌅",
            PrayerKind.Dhuhr => "🌄",
            PrayerKind.Asr => "🌇",
            PrayerKind.Maghrib => "🌆",
            PrayerKind.Isha => "🌌",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}