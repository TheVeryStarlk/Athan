using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    // Initialized by another method
    [ObservableProperty]
    private INavigable navigable = null!;

    private readonly NavigationService navigationService;
    private readonly SettingsService settingsService;
    private readonly LocationViewModel locationViewModel;
    private readonly DashboardViewModel dashboardViewModel;
    private readonly OfflineViewModel offlineViewModel;

    public ShellViewModel(NavigationService navigationService, SettingsService settingsService,
        LocationViewModel locationViewModel, DashboardViewModel dashboardViewModel, OfflineViewModel offlineViewModel)
    {
        this.navigationService = navigationService;
        this.settingsService = settingsService;
        this.locationViewModel = locationViewModel;
        this.dashboardViewModel = dashboardViewModel;
        this.offlineViewModel = offlineViewModel;

        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    private void NetworkChangeOnNetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs eventArgs)
    {
        Navigable = eventArgs.IsAvailable
            ? navigationService.GoBackward() ?? locationViewModel
            : navigationService.GoForward(offlineViewModel);
    }

    [RelayCommand]
    private async Task InitializedAsync()
    {
        var settings = await settingsService.ReadAsync();

        Navigable = navigationService.GoForward(settings.Location is null
            ? locationViewModel
            : dashboardViewModel);
    }
}