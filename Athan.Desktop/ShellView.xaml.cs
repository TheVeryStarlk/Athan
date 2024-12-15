using Wpf.Ui.Appearance;

namespace Athan.Desktop;

public sealed partial class ShellView
{
    public ShellView(ShellViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        ApplicationThemeManager.Apply(this);
    }
}