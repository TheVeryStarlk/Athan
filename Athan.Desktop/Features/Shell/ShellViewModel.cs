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
    SettingsService settingsService,
    WelcomeViewModel welcomeViewModel,
    PrayersViewModel prayersViewModel,
    SettingsViewModel settingsViewModel,
    OfflineViewModel offlineViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = welcomeViewModel;

    public async Task InitializeAsync()
    {
        navigationService.Navigate( await settingsService.LoadAsync() is null
            ? Destination.Welcome
            : Destination.Prayers);

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