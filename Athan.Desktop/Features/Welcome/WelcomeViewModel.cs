using Athan.Desktop.Extensions;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;
using Wpf.Ui;
using Wpf.Ui.Controls;
using NavigationService = Athan.Desktop.Features.Shell.NavigationService;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeViewModel(
    ILogger logger,
    LocationService locationService,
    SnackbarService snackbarService,
    SettingsService settingsService,
    NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private async Task StartAsync()
    {
        var result = await locationService.GetLocationAsync();

        if (result.IsFailed)
        {
            snackbarService.Show(
                "Failed to retrieve location",
                result.Errors[0].Message,
                SymbolRegular.Warning20);

            return;
        }

        snackbarService.Show(
            "Location retrieved",
            $"You are in {result.Value}.",
            SymbolRegular.Location20);

        logger.Error("Saving and navigating to prayers...");

        settingsService.Set(nameof(Location), result.Value);
        navigationService.Navigate(Destination.Prayers);
    }
}