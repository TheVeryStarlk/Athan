using System.Net.NetworkInformation;
using Athan.Avalonia.Contracts;
using Athan.Avalonia.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    // It gets initialized by its property
    [ObservableProperty]
    private INavigable navigable = null!;

    private readonly NavigationService navigationService;
    private readonly LocationViewModel locationViewModel;
    private readonly OfflineViewModel offlineViewModel;

    public ShellViewModel(NavigationService navigationService, LocationViewModel locationViewModel,
        OfflineViewModel offlineViewModel)
    {
        this.navigationService = navigationService;
        this.locationViewModel = locationViewModel;
        this.offlineViewModel = offlineViewModel;

        Navigable = navigationService.GoForward(locationViewModel);
        NetworkChange.NetworkAvailabilityChanged += NetworkChangeOnNetworkAvailabilityChanged;
    }

    private void NetworkChangeOnNetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs eventArgs)
    {
        Navigable = eventArgs.IsAvailable
            ? navigationService.GoBackward() ?? locationViewModel
            : navigationService.GoForward(offlineViewModel);
    }
}