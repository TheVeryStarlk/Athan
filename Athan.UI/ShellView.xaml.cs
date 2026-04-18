using WinUIEx;

namespace Athan.UI;

internal sealed partial class ShellView : WindowEx
{
    private readonly ShellViewModel viewModel;

    public ShellView(ShellViewModel viewModel)
    {
        this.viewModel = viewModel;

        InitializeComponent();

        this.CenterOnScreen();
    }
}