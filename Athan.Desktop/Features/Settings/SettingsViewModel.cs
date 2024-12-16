using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsViewModel : ObservableObject
{
    [RelayCommand]
    private void Save()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Welcome));
    }
}