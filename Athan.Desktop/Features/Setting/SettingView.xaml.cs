using System.Windows.Controls;

namespace Athan.Desktop.Features.Setting;

public sealed partial class SettingView : UserControl
{
    public SettingView(SettingViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}