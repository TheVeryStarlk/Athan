using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class DashboardViewModel : ObservableObject, INavigable
{
    public string Title => DateTime.Now.DayOfWeek.ToString();

    [ObservableProperty]
    private string? location;

    [ObservableProperty]
    private string date;

    [ObservableProperty]
    private string? prayer;

    private readonly SettingsService settingsService;
    private readonly PrayerService prayerService;

    public DashboardViewModel(SettingsService settingsService, PrayerService prayerService)
    {
        this.settingsService = settingsService;
        this.prayerService = prayerService;

        var calendar = new HijriCalendar();

        var hijri =
            $"{calendar.GetYear(DateTime.Now)}/" +
            $"{calendar.GetMonth(DateTime.Now)}/" +
            $"{calendar.GetDayOfMonth(DateTime.Now)}";

        date = DateTime.Parse(hijri).ToShortDateString();
    }

    [RelayCommand]
    private async Task InitializedAsync()
    {
        var settings = await settingsService.ReadAsync();

        var (city, country) = settings.Location!;
        Location = $"{city}, {country}";

        var prayerTimings = await prayerService.GetTimingsAsync(city, country);

        var closestTime = prayerTimings
            .OrderBy(timing => Math.Abs((timing.Time - DateTime.Now).Ticks))
            .First();

        Prayer = closestTime.Name;
    }
}