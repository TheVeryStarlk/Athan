using Athan.UI.Features.Prayers;
using Athan.UI.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.UI.Features.Welcome;

internal sealed partial class WelcomeViewModel(
    LocationService locationService,
    PrayerViewModel prayerViewModel) : ObservableObject
{
    [RelayCommand]
    private void Done()
    {
        var request = new NavigationRequest(prayerViewModel);
        WeakReferenceMessenger.Default.Send(request);
    }
}