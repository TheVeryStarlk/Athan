using System;
using System.Collections.Frozen;
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
    public partial string? Description { get; set; }

    [ObservableProperty]
    public partial PrayerKind? Upcoming { get; set; }

    [ObservableProperty]
    public partial string? Remaining { get; set; }

    private FrozenDictionary<PrayerKind, DateTimeOffset>? times;

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
        timerService.Start();
        timerService.Tick += Refresh;

        Prayers.Clear();

        var now = timeProvider.GetLocalNow();

        Description = now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);

        var calculator = new PrayerTimesCalculator(settingsService.Method.ToOptions());

        times = calculator.Calculate(now, Location.Latitude, Location.Longitude);

        foreach (var pair in times)
        {
            Prayers.Add(new Prayer(pair.Key, pair.Value.ToLocalTime().ToString("h:mm tt")));
        }

        // Force a refresh; Avoids in case of waiting for a minute to refresh.
        Refresh();
    }

    private void Refresh()
    {
        ArgumentNullException.ThrowIfNull(times);

        var now = timeProvider.GetLocalNow();

        times.FirstOrDefault(pair => pair.Value > now).Deconstruct(out var kind, out var time);

        // Show how much is left for Fajr in the next day.
        if (time == default)
        {
            time = times[PrayerKind.Fajr].AddDays(1);
        }

        Glyph = Prayer.ToEmoji(kind);

        Upcoming = kind;
        Remaining = (time - now).ToReadable();
    }
}

internal sealed class Prayer(PrayerKind kind, string time)
{
    public PrayerKind Kind => kind;

    public string Time => time;

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