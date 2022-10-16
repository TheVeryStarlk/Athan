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
    private INavigable navigable;

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

        Navigable = navigationService.GoForward(locationViewModel);
        SetProperty(ref navigable, Navigable);

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

        Navigable = navigationService.GoForward(string.IsNullOrWhiteSpace(settings.Location)
            ? locationViewModel
            : dashboardViewModel);
    }
}