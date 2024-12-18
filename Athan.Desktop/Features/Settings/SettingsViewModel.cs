using Athan.Desktop.Features.Shell;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsViewModel(SettingsService settingsService, NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private void Clear()
    {
        settingsService.Delete();
        navigationService.Navigate(Destination.Welcome);
    }

    [RelayCommand]
    private void Save()
    {
        navigationService.NavigateBackward();
    }
}