using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Desktop.Features.Setting;

public sealed partial class SettingViewModel : ObservableObject
{
    [RelayCommand]
    private void Save()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Welcome));
    }
}