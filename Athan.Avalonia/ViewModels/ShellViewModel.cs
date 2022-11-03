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
    private readonly SettingService settingService;
    private readonly LocationViewModel locationViewModel;
    private readonly DashboardViewModel dashboardViewModel;
    private readonly OfflineViewModel offlineViewModel;

    public ShellViewModel(NavigationService navigationService, SettingService settingService,
        LocationViewModel locationViewModel, DashboardViewModel dashboardViewModel, OfflineViewModel offlineViewModel)
    {
        this.navigationService = navigationService;
        this.settingService = settingService;
        this.locationViewModel = locationViewModel;
        this.dashboardViewModel = dashboardViewModel;
        this.offlineViewModel = offlineViewModel;

        navigationService.Navigated += toNavigate => Navigable = toNavigate;
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    [RelayCommand]
    private async Task InitializeAsync()
    {
        var settings = await settingService.ReadAsync();

        if (settings is null)
        {
            navigationService.GoForward(locationViewModel);
        }
        else
        {
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