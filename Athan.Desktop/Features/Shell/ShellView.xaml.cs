using Wpf.Ui.Appearance;

namespace Athan.Desktop.Features.Shell;

public sealed partial class ShellView
{
    public ShellView(ShellViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();

        ApplicationThemeManager.Apply(this);
    }
}