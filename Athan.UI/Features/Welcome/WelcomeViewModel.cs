using Athan.UI.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Welcome;

internal sealed partial class WelcomeViewModel(LocationService locationService) : ObservableObject
{
    [RelayCommand]
    private void Done()
    {
        WeakReferenceMessenger.Default.Send<ReadyMessage>();
    }
}