using System.Text;
using System.Timers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Athan.Desktop.Extensions;
using CommunityToolkit.Mvvm.Messaging;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;
using Timer = System.Timers.Timer;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer? NextPrayer { get; set; }

    [ObservableProperty]
    public partial string? When { get; set; }

    [ObservableProperty]
    public partial Prayer[]? Prayers { get; set; }

    private bool firstTime = true;

    private readonly Timer timer = new()
    {
        Interval = TimeSpan.FromSeconds(1).TotalMilliseconds,
        AutoReset = true
    };

    private readonly PrayerService prayerService;
    private readonly SettingsService settingsService;
    private readonly SnackbarService snackbarService;
    private readonly NotificationService notificationService;

    public PrayersViewModel(
        NavigationService navigationService,
        PrayerService prayerService,
        SettingsService settingsService,
        SnackbarService snackbarService,
        NotificationService notificationService)
    {
        this.prayerService = prayerService;
        this.settingsService = settingsService;
        this.snackbarService = snackbarService;
        this.notificationService = notificationService;

        navigationService.Navigated += destination =>
        {
            if (destination is not Destination.Prayers || !firstTime)
            {
                return;
            }

            UpdateAsync();
            timer.Start();
        };

        WeakReferenceMessenger.Default.Register<PrayersViewModel, Closing>(
            this,
            static (self, _) => self.timer.Dispose());

        timer.Elapsed += (_, _) => UpdateAsync();
    }

    private async void UpdateAsync()
    {
        try
        {
            firstTime = false;
            timer.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;

            await UpdatePrayersAsync();
            NextPrayer = GetNextPrayer(Prayers!);

            var now = DateTime.Now;

            if (now > Prayers!.Last().Time)
            {
                now = now.Subtract(TimeSpan.FromDays(1));
            }

            var difference = NextPrayer!.Time > now
                ? NextPrayer!.Time - now
                : now - NextPrayer!.Time;

            UpdateWhen(difference);

            if (difference.TotalSeconds < 30 && settingsService.Get<bool>("EnableNotifications"))
            {
                await notificationService.ShowAsync("Prayer time", $"Now is the prayer time for {NextPrayer.Name}.");
            }
        }
        catch
        {
            snackbarService.Show(
                "Failed to update",
                "An error has occured while updating the timings",
                SymbolRegular.Warning20);
        }
    }

    private async Task UpdatePrayersAsync()
    {
        var location = settingsService.Get<Location>(nameof(Location))!;
        var result = await prayerService.GetPrayersAsync(location.City, location.Country);

        if (result.IsFailed)
        {
            snackbarService.Show(
                "Failed to get prayer times",
                result.Errors[0].Message,
                SymbolRegular.Warning20);

            return;
        }

        Prayers = result.Value;
        NextPrayer = GetNextPrayer(Prayers);
    }

    private void UpdateWhen(TimeSpan difference)
    {
        var builder = new StringBuilder("After ");
        var hours = (int) difference.TotalHours;

        if (hours > 0)
        {
            builder.Append($"{hours} hours");
        }

        var minutes = (int) (difference.TotalMinutes % 60) % 60;

        if (minutes > 0)
        {
            builder.Append($" and {minutes} minutes");
        }

        When = builder.ToString();
    }

    private static Prayer GetNextPrayer(Prayer[] prayers)
    {
        var now = DateTime.Now;

        var closestPrayer = prayers
            .OrderBy(timing => Math.Abs((timing.Time - now).Ticks))
            .First();

        if (now <= closestPrayer.Time)
        {
            return closestPrayer;
        }

        var index = prayers.TakeWhile(prayer => prayer != closestPrayer).Count() + 1;
        return index >= prayers.Length ? prayers[0] : prayers[index];
    }
}