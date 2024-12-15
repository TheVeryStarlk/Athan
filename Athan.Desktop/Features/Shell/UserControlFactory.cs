using System.ComponentModel;
using System.Windows.Controls;
using Athan.Desktop.Features.Setting;
using Athan.Desktop.Features.Welcome;

namespace Athan.Desktop.Features.Shell;

public sealed class UserControlFactory(
    Func<WelcomeViewModel, WelcomeView> welcomeViewFactory,
    Func<SettingViewModel, SettingView> settingViewFactory)
{
    public UserControl Create(INotifyPropertyChanged viewModel)
    {
        return viewModel switch
        {
            WelcomeViewModel current => welcomeViewFactory(current),
            SettingViewModel current => settingViewFactory(current),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}