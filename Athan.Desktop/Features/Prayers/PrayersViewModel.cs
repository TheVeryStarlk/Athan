using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;
using Athan.Desktop.Extensions;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersViewModel : ObservableObject
{
    [ObservableProperty]
    public partial Prayer? NextPrayer { get; set; }

    [ObservableProperty]
    public partial Prayer[]? Prayers { get; set; }

    private readonly PrayerService prayerService;
    private readonly SettingsService settingsService;
    private readonly SnackbarService snackbarService;

    public PrayersViewModel(
        NavigationService navigationService,
        PrayerService prayerService,
        SettingsService settingsService,
        SnackbarService snackbarService)
    {
        this.prayerService = prayerService;
        this.settingsService = settingsService;
        this.snackbarService = snackbarService;

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
        NextPrayer = GetClosest(Prayers);
    }

    private Prayer GetClosest(Prayer[] prayers)
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