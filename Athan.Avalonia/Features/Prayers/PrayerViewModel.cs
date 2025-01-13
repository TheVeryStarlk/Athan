using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Athan.Avalonia.Extensions;
using Athan.Avalonia.Features.Prayers.Retry;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using DesktopNotifications;
using Serilog;

namespace Athan.Avalonia.Features.Prayers;

internal sealed partial class PrayerViewModel(
    ILogger logger,
    INotificationManager notificationManager,
    PrayerService prayerService,
    LocationService locationService,
    StorageService storageService,
    RetryViewModel retryViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private readonly string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];

    public async Task InitializeAsync()
    {
        using var source = new CancellationTokenSource();

        while (true)
        {
            var message = "";

            if (!storageService.TryGet("Location", out Location? location))
            {
                logger.Information("Getting location.");

                var result = await locationService.GetAsync();

                if (!result.IsSuccess(out location))
                {
                    message = result.Errors.First().Message;

                    await source.CancelAsync();
                }

                storageService.Set("Location", location);
            }

            while (!source.IsCancellationRequested)
            {
                try
                {
                    logger.Information("Getting prayer timings.");

                    var result = await prayerService.GetAsync(location!.Country, location.City);

                    if (!result.IsSuccess(out var prayers))
                    {
                        message = result.Errors.First().Message;
                        break;
                    }

                    var filtered = prayers.Where(prayer => main.Contains(prayer.Key))
                        .ToArray();

                    var now = DateTime.Now.Subtract(TimeSpan.FromDays(1));

                    Prayers = filtered.Select(prayer => new Prayer(prayer.Key, prayer.Value - now))
                        .ToArray();

                    var next = Prayers.OrderBy(prayer => prayer.When.Hours)
                        .First();

                    Next = next;

                    logger.Information("Update scheduled after {When}.", next.When);

                    await Task.Delay(next.When, source.Token);

                    await notificationManager.ShowAsync("Prayer time",
                        $"Now is the prayer time for {Next.Name}.");
                }
                catch (Exception exception)
                {
                    logger.Fatal(exception, "A fatal exception occured.");
                    message = "Something went wrong...";

                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            await retryViewModel.ShowAsync(message);
        }
    }
}