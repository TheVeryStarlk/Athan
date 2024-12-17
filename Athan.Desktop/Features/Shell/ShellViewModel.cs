using System.ComponentModel;
using System.Net.NetworkInformation;
using Athan.Desktop.Features.Offline;
using Athan.Desktop.Features.Prayers;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel(
    NavigationService navigationService,
    WelcomeViewModel welcomeViewModel,
    PrayersViewModel prayersViewModel,
    SettingsViewModel settingsViewModel,
    OfflineViewModel offlineViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = welcomeViewModel;

    public void Initialize()
    {
        navigationService.Navigate(Destination.Welcome);

        navigationService.Navigated += destination => Current = destination switch
        {
            Destination.Welcome => welcomeViewModel,
            Destination.Prayers => prayersViewModel,
            Destination.Settings => settingsViewModel,
            Destination.Offline => offlineViewModel,
            _ => throw new ArgumentOutOfRangeException()
        };

        NetworkChange.NetworkAvailabilityChanged += (_, eventArgs) =>
        {
            if (eventArgs.IsAvailable)
            {
                navigationService.NavigateBackward();
            }
            else
            {
                navigationService.Navigate(Destination.Offline);
            }
        };
    }

    [RelayCommand]
    private void NavigateSettings()
    {
        navigationService.Navigate(Destination.Settings);
    }
}