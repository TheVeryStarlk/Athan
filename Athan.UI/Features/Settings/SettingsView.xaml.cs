using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI.Features.Settings;

internal sealed partial class SettingsView : Page
{
    private SettingsViewModel? viewModel;

    public SettingsView()
    {
        InitializeComponent();
    }
    
    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        viewModel = (SettingsViewModel) eventArgs.Parameter;
        base.OnNavigatedTo(eventArgs);
    }
}