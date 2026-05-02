using System;
using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersViewModel : HeaderViewModel
{
    public ObservableCollection<Prayer> Prayers { get; } =
    [
        new("Fajr", "4:00 AM"),
        new("Duhur", "12:00 PM"),
        new("Asr", "3:00 PM"),
        new("Maghrib", "6:00 PM"),
        new("Isha", "9:00 PM")
    ];

    [ObservableProperty]
    public partial string? Hijri { get; set; }

    [RelayCommand]
    private void Initialize()
    {
        Hijri = DateTimeOffset.Now.ToString(CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern, new CultureInfo("ar-SA"));
    }
}

internal sealed class Prayer(string name, string time)
{
    public string Name => name;

    public string Time => time;
}