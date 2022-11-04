using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private INavigable? navigable;

    private readonly NavigationService navigationService;
    private readonly SettingsService settingsService;
    private readonly LocationViewModel locationViewModel;
    private readonly DashboardViewModel dashboardViewModel;
    private readonly OfflineViewModel offlineViewModel;
    private readonly ThemeService themeService;

    public ShellViewModel(NavigationService navigationService, SettingsService settingsService,
        LocationViewModel locationViewModel, DashboardViewModel dashboardViewModel, 
        OfflineViewModel offlineViewModel, ThemeService themeService)
    {
        this.navigationService = navigationService;
        this.settingsService = settingsService;
        this.locationViewModel = locationViewModel;
        this.dashboardViewModel = dashboardViewModel;
        this.offlineViewModel = offlineViewModel;
        this.themeService = themeService;

        navigationService.Navigated += toNavigate => Navigable = toNavigate;
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var settings = await settingsService.ReadAsync();

        if (settings?.Location is null)
        {
            navigationService.GoForward(locationViewModel);
        }
        else
        {
            themeService.Update(settings.Theme);
            await navigationService.GoForwardAsync(dashboardViewModel, settings);
        }
    }

    private void NetworkChangeOnNetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs eventArgs)
    {
        if (eventArgs.IsAvailable)
        {
            navigationService.GoBackward();
        }
        else
        {
            navigationService.GoForward(offlineViewModel);
        }
    }
}