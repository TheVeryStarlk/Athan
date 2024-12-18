using System.ComponentModel;
using System.Net.NetworkInformation;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Prayers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel(
    ILogger logger,
    NavigationService navigationService,
    SettingsService settingsService,
    NotificationService notificationService,
    WelcomeViewModel welcomeViewModel,
    PrayersViewModel prayersViewModel,
    SettingsViewModel settingsViewModel,
    OfflineViewModel offlineViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = welcomeViewModel;

    public async Task InitializeAsync()
    {
        logger.Information("Initializing shell view-model");

        await settingsService.InitializeAsync();
        await notificationService.InitializeAsync();

        var location = settingsService.Get<Location>(nameof(Location));

        navigationService.Navigate(location is null
            ? Destination.Welcome
            : Destination.Prayers);

        navigationService.Navigated += destination => Current = destination switch
        {
            Destination.Welcome => welcomeViewModel,
            Destination.Prayers => prayersViewModel,
            Destination.Settings => settingsViewModel,
            Destination.Offline => offlineViewModel,
            _ => throw new ArgumentOutOfRangeException()
        };

        NetworkChange.NetworkAvailabilityChanged += (_, eventArgs) =>
        {
            logger.Information("Network availability changed");

            if (eventArgs.IsAvailable)
            {
                navigationService.NavigateBackward();
            }
            else
            {
                navigationService.Navigate(Destination.Offline);
            }
        };
    }

    public async Task OnClosingAsync()
    {
        logger.Information("Closing shell view-model");
        await settingsService.SaveAsync();
    }

    public async Task OnMinimizedAsync()
    {
        logger.Information("Minimizing shell view-model");

        if (!settingsService.Get<bool>("EnableNotifications"))
        {
            return;
        }

        await notificationService.ShowAsync(
            "Athan is still running",
            "You can open Athan from the tray icon menu.");
    }

    [RelayCommand]
    private void NavigateSettings()
    {
        if (Current is SettingsViewModel)
        {
            return;
        }

        navigationService.Navigate(Destination.Settings);
    }
}