using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Athan.Avalonia.Models;
using Athan.Avalonia.Services;
using Athan.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class PrayerViewModel(
    ILogger logger,
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
        WeakReferenceMessenger.Default.Register(this);
        task = Task.Factory.StartNew(StartAsync, TaskCreationOptions.LongRunning);
    }

    private async Task StartAsync()
    {
        try
        {
            if (!storageService.TryGet("Location", out Location? value))
            {
                logger.Information("Getting location.");

                var location = await locationService.GetAsync();

                if (!location.IsSuccess(out value))
                {
                    throw new Exception("Unable to get location.");
                }

                storageService.Set("Location", value);
            }

            while (!source.IsCancellationRequested)
            {
                logger.Information("Getting prayer timings.");

                var result = await prayerService.GetAsync(value.Country, value.City);

                if (!result.IsSuccess(out var prayers))
                {
                    throw new Exception("Unable to get prayer timings.");
                }

                var filtered = prayers
                    .Where(prayer => main.Contains(prayer.Key))
                    .ToArray();

                var now = DateTime.Now.Subtract(TimeSpan.FromDays(1));

                Prayers = filtered
                    .Select(prayer => new Prayer(prayer.Key, prayer.Value - now))
                    .ToArray();

                var next = Prayers
                    .OrderBy(prayer => prayer.When.Hours)
                    .ToArray()
                    .First();

                Next = next;

                logger.Information("Update scheduled after {When}.", next.When);

                await Task.Delay(next.When, source.Token);
            }
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            WeakReferenceMessenger.Default.Send(new Error(exception.Message));
        }
    }

    public void Receive(Closing closing)
    {
        source.Cancel();
        task?.Dispose();
    }
}