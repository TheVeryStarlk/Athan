using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Models;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private readonly PrayerService prayerService;
    private readonly LocationService locationService;

    private readonly Timer timer = new();
    private readonly string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];

    public PrayerViewModel(PrayerService prayerService, LocationService locationService)
    {
        this.prayerService = prayerService;
        this.locationService = locationService;

        timer.Elapsed += async (_, _) => await InitializeAsync();
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var result = await locationService
            .GetAsync()
            .ThenAsync(async location => await prayerService.GetAsync(location.Country, location.City));

        if (!result.IsSuccess(out var prayers))
        {
            return;
        }

        var filtered = prayers
            .Where(prayer => main.Contains(prayer.Key))
            .ToArray();

        var now = DateTime.Now.Subtract(TimeSpan.FromDays(1));

        Prayers = filtered
            .Select(prayer => new Prayer(prayer.Key, prayer.Value - now))
            .ToArray();

        var next = Prayers
            .Where(prayer => prayer.When.Ticks > 0)
            .OrderBy(prayer => prayer.When.Ticks)
            .First();

        Next = next;

        timer.Interval = next.When.TotalMilliseconds;
        timer.Enabled = true;
    }
}