using System.Net.NetworkInformation;
using Athan.Avalonia.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Avalonia.ViewModels;

internal sealed partial class ShellViewModel : ObservableObject
{
    private readonly YouSeemToBeOfflineViewModel youSeemToBeOfflineViewModel;

    [ObservableProperty]
    private INavigable? navigable;

    public ShellViewModel(YouSeemToBeOfflineViewModel youSeemToBeOfflineViewModel)
    {
        this.youSeemToBeOfflineViewModel = youSeemToBeOfflineViewModel;
    }

    [RelayCommand]
    private void CheckForInternetConnection()
    {
        if (NetworkInterface.GetIsNetworkAvailable())
        {
            Navigable = youSeemToBeOfflineViewModel;
        }
    }
}