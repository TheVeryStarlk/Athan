using System.ComponentModel;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel(
    NavigationService navigationService,
    WelcomeViewModel welcomeViewModel,
    SettingsViewModel settingsViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = welcomeViewModel;

    public void Initialize()
    {
        navigationService.Navigate(Destination.Welcome);

        navigationService.Navigated += destination => Current = destination switch
        {
            Destination.Welcome => welcomeViewModel,
            Destination.Settings => settingsViewModel,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    [RelayCommand]
    private void NavigateSettings()
    {
        navigationService.Navigate(Destination.Settings);
    }
}