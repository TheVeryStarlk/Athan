using System.ComponentModel;
using Athan.Desktop.Features.Setting;
using Athan.Desktop.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellViewModel(
    WelcomeViewModel welcomeViewModel,
    SettingViewModel settingViewModel) : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged Current { get; set; } = welcomeViewModel;
}