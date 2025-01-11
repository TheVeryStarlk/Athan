using Avalonia.Controls;
using Avalonia.Input;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellView : Window
{
    public ShellView(ShellViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    private void CaptionBorderOnPointerReleased(object? sender, PointerReleasedEventArgs eventArgs)
    {
        var border = (CaptionBorder) sender!;

        if (border.Type is CaptionType.Close)
        {
            Close();
        }
        else
        {
            WindowState = WindowState.Minimized;
        }
    }
}