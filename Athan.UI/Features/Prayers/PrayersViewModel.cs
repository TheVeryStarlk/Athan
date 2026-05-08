using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Prayers.Calculation;
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

    private readonly TimeProvider timeProvider;

    public PrayersViewModel(Location location, TimeProvider timeProvider)
    {
        Location = location;

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

        Hijri = now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));

        var calculator = new PrayerTimesCalculator(MakkahPrayerTimesCalculatorOptions.Instance);
        var times = calculator.Calculate(now, Location.Latitude, Location.Longitude);

        foreach (var pair in times)
        {
            Prayers.Add(new Prayer(pair.Key.ToString(), pair.Value.ToLocalTime().ToString("h:mm tt")));
        }

        var upcoming = times.FirstOrDefault(time => time.Value > now);

        if (upcoming.Equals(default(KeyValuePair<Calculation.Prayer, DateTimeOffset>)))
        {
            upcoming = calculator.Calculate(now.AddDays(1), Location.Latitude, Location.Longitude).First();
        }

        Upcoming = upcoming.Key.ToString();

        var left = upcoming.Value - now;
        var hours = (int) left.TotalHours;
        var minutes = left.Minutes;

        var result = (hours, minutes) switch
        {
            (> 0, > 0) => $"{hours} hours and {minutes} minutes left",
            (> 0, 0) => $"{hours} hours left",
            (0, > 0) => $"{minutes} minutes left",
            (0, 0) => "Less than a minute left",
            _ => throw new ArgumentOutOfRangeException()
        };

        Message = result;
    }
}

internal sealed class Prayer(string name, string time)
{
    public string Name => name;

    public string Time => time;
}
