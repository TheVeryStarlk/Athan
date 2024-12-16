using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsViewModel(NavigationService navigationService) : ObservableObject
{
    [RelayCommand]
    private void Save()
    {
        navigationService.NavigateBackward();
    }
}