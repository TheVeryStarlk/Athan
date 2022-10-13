using System.Net.NetworkInformation;
using Athan.Avalonia.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    private readonly OfflineViewModel offlineViewModel;

    [ObservableProperty]
    private INavigable navigable;

    public ShellViewModel(LocationViewModel locationViewModel, OfflineViewModel offlineViewModel)
    {
        SetProperty(ref navigable, NetworkInterface.GetIsNetworkAvailable()
            ? locationViewModel
            : offlineViewModel);

        this.offlineViewModel = offlineViewModel;
    }

    [RelayCommand]
    private void CheckForInternetConnection()
    {
        if (!NetworkInterface.GetIsNetworkAvailable())
        {
            Navigable = offlineViewModel;
        }
    }
}