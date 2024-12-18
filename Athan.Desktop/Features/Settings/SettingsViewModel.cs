using Athan.Desktop.Features.Shell;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial bool EnableNotifications { get; set; }

    private readonly SettingsService settingsService;
    private readonly NavigationService navigationService;

    public SettingsViewModel(SettingsService settingsService, NavigationService navigationService)
    {
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
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        settingsService.Set(nameof(EnableNotifications), EnableNotifications);

        await settingsService.SaveAsync();
        navigationService.NavigateBackward();
    }
}