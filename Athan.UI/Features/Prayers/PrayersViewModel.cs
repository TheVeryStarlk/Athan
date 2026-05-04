using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Athan.UI.Features.Locations;
using Athan.UI.Features.Prayers.Calculation;
using Athan.UI.Features.Shell.Items;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersViewModel : HeaderViewModel
{
    public ObservableCollection<Prayer> Prayers { get; } = [];

    [ObservableProperty]
    public partial string? Hijri { get; set; }

    private readonly Location location;

    public PrayersViewModel(Location location)
    {
        this.location = location;

        Title = location.Name;
        Glyph = "🌄";
        Deletable = true;
    }

    [RelayCommand]
    private void Initialize()
    {
        Prayers.Clear();
        Hijri = DateTimeOffset.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));

        var calculator = new PrayerTimesCalculator(MakkahPrayerTimesCalculatorOptions.Instance);
        var times = calculator.Calculate(DateTimeOffset.Now, location.Latitude, location.Longitude);

        foreach (var pair in times)
        {
            Prayers.Add(new Prayer(pair.Key.ToString(), pair.Value.ToLocalTime().ToString("h:mm tt")));
        }
    }
}

internal sealed class Prayer(string name, string time)
{
    public string Name => name;

    public string Time => time;
}