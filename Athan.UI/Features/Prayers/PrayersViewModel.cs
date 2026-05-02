using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Athan.UI.Features.Prayers.Calculation;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersViewModel(Location location) : HeaderViewModel
{
    public ObservableCollection<PrayerItem> Prayers { get; } = [];

    [ObservableProperty]
    public partial string? Hijri { get; set; }

    [RelayCommand]
    private void Initialize()
    {
        Title = location.Name;
        Glyph = "🌄";
        Hijri = DateTimeOffset.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));

        var calculator = new PrayerTimesCalculator(MakkahPrayerTimesCalculatorOptions.Instance);
        var times = calculator.Calculate(DateTimeOffset.Now, location.Latitude, location.Longitude);

        foreach (var pair in times)
        {
            Prayers.Add(new PrayerItem(pair.Key.ToString(), pair.Value.ToLocalTime().ToString("h:mm tt")));
        }
    }
}

internal sealed class PrayerItem(string name, string time)
{
    public string Name => name;

    public string Time => time;
}