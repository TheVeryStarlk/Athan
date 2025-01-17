using System;
using System.Collections.Frozen;
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

    private bool running = true;

    public async Task StartAsync()
    {
        while (running)
        {
            var result = await GetLocationAsync().ThenAsync(GetTimingsAsync);
            var message = string.Empty;

            if (!result.IsSuccess(out var pairs))
            {
                message = result.Errors.First().Message;
            }

            while (string.IsNullOrWhiteSpace(message))
            {
                await RefreshStatusAsync(pairs!);
                await Task.Delay(TimeSpan.FromMinutes(1));
            }

            await retryViewModel.ShowAsync(message);
        }
    }

    private async Task<Result<Location>> GetLocationAsync()
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

    private async Task<Result<FrozenDictionary<string, DateTime>>> GetTimingsAsync(Location location)
    {
        logger.Information("Getting prayer timings.");

        var result = await prayerService.GetAsync(location.Country, location.City);

        if (!result.IsSuccess(out var prayers))
        {
            return result.AsFailure<FrozenDictionary<string, DateTime>>();
        }

        var filtered = prayers
            .Where(prayer => main.Contains(prayer.Key))
            .ToFrozenDictionary();

        return Result.Success(filtered);
    }

    private async Task RefreshStatusAsync(FrozenDictionary<string, DateTime> pairs)
    {
        var reference = DateTime.Now;

        Prayers = new Prayer[5];

        for (var index = 0; index < pairs.Count; index++)
        {
            var pair = pairs.ElementAt(index);

            var difference = pair.Value - reference;

            if (difference.Ticks < 0)
            {
                difference = pair.Value - reference.Subtract(TimeSpan.FromDays(1));
            }

            var emoji = pair.Key switch
            {
                "Fajr" => "🌅",
                "Dhuhr" => "🌄",
                "Asr" => "🌇",
                "Maghrib" => "🌆",
                "Isha" => "🌌",
                _ => throw new ArgumentOutOfRangeException(nameof(pairs), "Unknown name.")
            };

            Prayers[index] = new Prayer(emoji, pair.Key, difference);
        }

        var next = Prayers
            .OrderBy(prayer => prayer.After.Hours)
            .First();

        Next = next;

        var late = reference.Add(next.After);
        var coming = late - reference;

        if (coming.TotalSeconds < 60)
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