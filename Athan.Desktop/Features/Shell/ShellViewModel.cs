using System.ComponentModel;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SettingsViewModel = Athan.Desktop.Features.Settings.SettingsViewModel;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; }

    private readonly WelcomeViewModel welcomeViewModel;
    private readonly SettingsViewModel settingsViewModel;

    public ShellViewModel(WelcomeViewModel welcomeViewModel, SettingsViewModel settingsViewModel)
    {
        this.welcomeViewModel = welcomeViewModel;
        this.settingsViewModel = settingsViewModel;

        Current = welcomeViewModel;

        WeakReferenceMessenger.Default.Register<ShellViewModel, NavigationRequest>(
            this,
            static (self, message) => self.Current = message.Destination switch
            {
                Destination.Welcome => self.welcomeViewModel,
                Destination.Settings => self.settingsViewModel,
                _ => throw new ArgumentOutOfRangeException()
            });

        // WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Welcome));
    }

    [RelayCommand]
    private void NavigateSettings()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Settings));
    }
}