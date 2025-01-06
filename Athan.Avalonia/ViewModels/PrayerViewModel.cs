using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel(
    PrayerService prayerService,
    LocationService locationService,
    StorageService storageService) : ObservableRecipient, IRecipient<Closing>
{
    [ObservableProperty]
    public partial Prayer[] Prayers { get; set; } = [];

    [ObservableProperty]
    public partial Prayer? Next { get; set; }

    private Task? task;

    private readonly string[] main = ["Fajr", "Dhuhr", "Asr", "Maghrib", "Isha"];
    private readonly CancellationTokenSource source = new();

    public void Initialize()
    {
        task = Task.Factory.StartNew(StartAsync, TaskCreationOptions.LongRunning);
    }

    private async Task StartAsync()
    {
        try
        {
            if (!storageService.TryGet("Location", out Location? value))
            {
                var location = await locationService.GetAsync();

                if (!location.IsSuccess(out value))
                {
                    return;
                }

                storageService.Set("Location", value);
            }

            while (!source.IsCancellationRequested)
            {
                var result = await prayerService.GetAsync(value.Country, value.City);

                if (!result.IsSuccess(out var prayers))
                {
                    break;
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

                await Task.Delay(next.When, source.Token);
            }
        }
        catch (Exception exception)
        {
            Debug.WriteLine(exception);
        }
    }

    public void Receive(Closing message)
    {
        source.Cancel();
        task?.Dispose();
    }
}