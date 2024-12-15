using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeViewModel : ObservableObject
{
    [RelayCommand]
    private void Done()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Setting));
    }
}