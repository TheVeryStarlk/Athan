using System.ComponentModel;
using Athan.Desktop.Features.Setting;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; }

    private readonly WelcomeViewModel welcomeViewModel;
    private readonly SettingViewModel settingViewModel;

    public ShellViewModel(WelcomeViewModel welcomeViewModel, SettingViewModel settingViewModel)
    {
        this.welcomeViewModel = welcomeViewModel;
        this.settingViewModel = settingViewModel;

        Current = welcomeViewModel;

        WeakReferenceMessenger.Default.Register<ShellViewModel, NavigationRequest>(
            this,
            static (self, message) => self.Current = message.Destination switch
            {
                Destination.Welcome => self.welcomeViewModel,
                Destination.Setting => self.settingViewModel,
                _ => throw new ArgumentOutOfRangeException()
            });

        // WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Welcome));
    }

    [RelayCommand]
    private void NavigateSetting()
    {
        WeakReferenceMessenger.Default.Send(new NavigationRequest(Destination.Setting));
    }
}