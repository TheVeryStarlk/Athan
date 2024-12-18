using System.Diagnostics;
using System.IO;
using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Serilog;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial bool EnableNotifications { get; set; }

    private readonly ILogger logger;
    private readonly SettingsService settingsService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(ILogger logger, SettingsService settingsService, NavigationService navigationService)
    {
        this.logger = logger;
        this.settingsService = settingsService;
        this.navigationService = navigationService;

        navigationService.Navigated += destination =>
        {
            if (destination is Destination.Settings)
            {
                EnableNotifications = this.settingsService.Get<bool>(nameof(EnableNotifications));
            }
        };
    }

    [RelayCommand]
    private void Clear()
    {
        settingsService.Set<Location>(nameof(Location), null);
        navigationService.Navigate(Destination.Welcome);

        logger.Information("Cleared location");
    }

    [RelayCommand]
    private void OpenLogs()
    {
        logger.Information("Opening log file");

        var path = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "Athan");

        Process.Start("explorer.exe", path);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        logger.Information("Saving settings");

        settingsService.Set(nameof(EnableNotifications), EnableNotifications);

        await settingsService.SaveAsync();
        navigationService.NavigateBackward();
    }
}