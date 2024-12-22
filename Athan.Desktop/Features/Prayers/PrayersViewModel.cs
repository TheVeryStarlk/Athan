using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;
using Athan.Desktop.Extensions;
using CommunityToolkit.Mvvm.Messaging;
using Timer = System.Timers.Timer;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer? NextPrayer { get; set; }

    [ObservableProperty]
    public partial Prayer[]? Prayers { get; set; }

    private Timer timer = new()
    {
        AutoReset = true,
        Enabled = true
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

        WeakReferenceMessenger.Default.Register<PrayersViewModel, Closing>(
            this,
            static (self, _) => self.timer.Dispose());

        navigationService.Navigated += async destination =>
        {
            if (destination is Destination.Prayers)
            {
                await InitializeAsync();
            }
        };
    }

    private async Task InitializeAsync()
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

        timer.Interval = Math.Abs((NextPrayer!.Time - DateTime.Now).TotalMilliseconds);

        timer.Elapsed += async (_, _) =>
        {
            if (settingsService.Get<bool>("EnableNotifications"))
            {
                await notificationService.ShowAsync("Prayer time", $"Now is the prayer time for {NextPrayer.Name}.");
            }

            NextPrayer = GetNextPrayer(Prayers);
            timer.Interval = Math.Abs((NextPrayer!.Time - DateTime.Now).TotalMilliseconds);
        };
    }

    private Prayer GetNextPrayer(Prayer[] prayers)
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