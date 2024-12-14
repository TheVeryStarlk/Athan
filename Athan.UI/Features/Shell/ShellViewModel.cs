using System.ComponentModel;
using Athan.UI.Features.Welcome;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    public partial INotifyPropertyChanged? Current { get; set; }

    public ShellViewModel(WelcomeViewModel welcomeViewModel)
    {
        Current = welcomeViewModel;
    }
}