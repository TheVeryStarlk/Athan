using System.ComponentModel;
using System.Windows.Controls;
using Athan.Desktop.Features.Settings;
using Athan.Desktop.Features.Welcome;

namespace Athan.Desktop.Features.Shell;

public sealed class UserControlFactory(
    Func<WelcomeViewModel, WelcomeView> welcomeViewFactory,
    Func<SettingsViewModel, SettingsView> settingViewFactory)
{
    public UserControl Create(INotifyPropertyChanged viewModel)
    {
        return viewModel switch
        {
            WelcomeViewModel current => welcomeViewFactory(current),
            SettingsViewModel current => settingViewFactory(current),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}