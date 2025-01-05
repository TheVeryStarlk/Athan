using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Athan.Avalonia.Messages;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private readonly PrayerService prayerService;
    private readonly LocationService locationService;
    private readonly StorageService storageService;

    private readonly Timer timer = new();
    private readonly string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];

    public PrayerViewModel(PrayerService prayerService, LocationService locationService, StorageService storageService)
    {
        this.prayerService = prayerService;
        this.locationService = locationService;
        this.storageService = storageService;

        timer.Elapsed += async (_, _) => await RefreshAsync();

        WeakReferenceMessenger.Default.Register<PrayerViewModel, NavigationRequest>(
            this,
            async (_, _) => await RefreshAsync());
    }

    private async Task RefreshAsync()
    {
        if (!storageService.TryGet("Location", out Location? location))
        {
            var task = await locationService.GetAsync();

            if (!task.IsSuccess(out location))
            {
                return;
            }

            storageService.Set("Location", location);
        }

        var result = await prayerService.GetAsync(location.Country, location.City);

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