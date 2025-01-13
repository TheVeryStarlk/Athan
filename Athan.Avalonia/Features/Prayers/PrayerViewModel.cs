using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Features.Prayers.Retry;
using Athan.Avalonia.Features.Shell;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DesktopNotifications;
using LightResults;
using Serilog;

namespace Athan.Avalonia.Features.Prayers;

internal sealed partial class PrayerViewModel(
    ILogger logger,
    INotificationManager notificationManager,
    PrayerService prayerService,
    LocationService locationService,
    StorageService storageService,
    RetryViewModel retryViewModel) : ObservableRecipient, IRecipient<Closing>
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private readonly string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];

    private KeyValuePair<string, DateTime>[] filtered = [];
    private bool running = true;

    public async Task StartAsync()
    {
        while (running)
        {
            var result = await RetrieveLocationAsync().ThenAsync(UpdatePrayerAsync);
            var message = "";

            if (!result.IsSuccess())
            {
                message = result.Errors.First().Message;
            }

            while (string.IsNullOrWhiteSpace(message))
            {
                await RefreshStatusAsync();
                await Task.Delay(TimeSpan.FromMinutes(1));
            }

            await retryViewModel.ShowAsync(message);
        }
    }

    private async Task<Result<Location>> RetrieveLocationAsync()
    {
        if (storageService.TryGet("Location", out Location? location))
        {
            logger.Information("Loaded location from settings.");
            return location;
        }

        logger.Information("Getting location.");

        var result = await locationService.GetAsync();

        if (!result.IsSuccess(out location))
        {
            return result.AsFailure<Location>();
        }

        storageService.Set("Location", location);

        return location;
    }

    private async Task<Result> UpdatePrayerAsync(Location location)
    {
        logger.Information("Getting prayer timings.");

        var result = await prayerService.GetAsync(location.Country, location.City);

        if (!result.IsSuccess(out var prayers))
        {
            return result.AsFailure();
        }

        filtered = prayers
            .Where(prayer => main.Contains(prayer.Key))
            .ToArray();

        return Result.Success();
    }

    private async Task RefreshStatusAsync()
    {
        var now = DateTime.Now;

        Prayers = filtered
            .Select(prayer => new Prayer(prayer.Key, prayer.Value - now))
            .ToArray();

        var next = Prayers
            .OrderBy(prayer => prayer.After.Hours)
            .First();

        Next = next;

        var late = now.Add(next.After);
        var difference = late - now;

        if (difference.TotalSeconds < 15)
        {
            logger.Information("Sent prayer notification.");

            await notificationManager.ShowAsync(
                "Prayer time",
                $"Now is the prayer time for {Next.Name}.");
        }

        logger.Debug("Updated status.");
    }

    public void Receive(Closing message)
    {
        running = false;
    }
}