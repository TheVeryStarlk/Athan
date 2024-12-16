using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeViewModel(NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private void Done()
    {
        navigationService.Navigate(Destination.Offline);
    }
}