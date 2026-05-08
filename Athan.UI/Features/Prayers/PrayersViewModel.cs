using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Prayers.Calculation;
using Athan.UI.Features.Settings;
using Athan.UI.Features.Shell.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Prayers;

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

    public PrayersViewModel(Location location, SettingsService settingsService, TimerService timerService, TimeProvider timeProvider)
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
        Refresh();

        var now = timeProvider.GetLocalNow();
        var elapsed = TimeSpan.FromTicks(now.TimeOfDay.Ticks % TimeSpan.FromMinutes(1).Ticks);
        var interval = elapsed == TimeSpan.Zero ? TimeSpan.FromMinutes(1) : TimeSpan.FromMinutes(1) - elapsed;

        timerService.Start(interval, Update);
    }

    [RelayCommand]
    private void Close()
    {
        timerService.Stop();
    }

    private void Update()
    {
        Refresh();
        timerService.Start(TimeSpan.FromMinutes(1), Update);
    }

    private void Refresh()
    {
        Prayers.Clear();

        var now = timeProvider.GetLocalNow();

        Hijri = now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));

        var options = settingsService.Get(PrayerCalculation.Makkah, AthanSerializerContext.Default.PrayerCalculation) switch
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
            Prayers.Add(new Prayer(pair.Key.ToString(), pair.Value.ToLocalTime().ToString("h:mm tt")));
        }

        var upcoming = times.First(time => time.Value > now);
        Upcoming = upcoming.Key.ToString();

        var left = upcoming.Value - now;
        var hours = (int) left.TotalHours;
        var minutes = left.Minutes;

        Message = (hours, minutes) switch
        {
            (> 0, > 0) => $"{hours} hours and {minutes} minutes left",
            (> 0, 0) => $"{hours} hours left",
            (0, > 0) => $"{minutes} minutes left",
            (0, 0) => "Less than a minute left",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

internal sealed class Prayer(string name, string time)
{
    public string Name => name;

    public string Time => time;
}