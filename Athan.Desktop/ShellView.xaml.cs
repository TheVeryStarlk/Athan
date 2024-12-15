using Wpf.Ui.Appearance;

namespace Athan.Desktop;

public sealed partial class ShellView
{
    public ShellView()
    {
        InitializeComponent();
        ApplicationThemeManager.Apply(this);
    }
}