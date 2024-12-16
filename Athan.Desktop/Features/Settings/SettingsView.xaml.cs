using System.Windows.Controls;

namespace Athan.Desktop.Features.Settings;

public sealed partial class SettingsView : UserControl
{
    public SettingsView(SettingsViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}