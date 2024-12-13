using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace Athan.UI.Features.Shell;

internal sealed partial class ShellView : Window
{
    public ShellViewModel ViewModel { get; }

    public ShellView(ShellViewModel viewModel)
    {
        ViewModel = viewModel;

        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SystemBackdrop = new MicaBackdrop();
    }
}