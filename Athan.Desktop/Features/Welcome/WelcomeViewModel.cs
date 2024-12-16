using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeViewModel(LocationService locationService, NavigationService navigationService) : ObservableObject
{
    [ObservableProperty]
    public partial string? Location { get; set; }

    [RelayCommand]
    private async Task StartAsync()
    {
        // var result = await locationService.GetLocationAsync();
        //
        // if (result.IsFailed)
        // {
        //     return;
        // }

        navigationService.Navigate(Destination.Offline);
    }
}