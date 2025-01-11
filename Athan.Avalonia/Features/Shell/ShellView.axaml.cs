using Avalonia.Controls;
using Avalonia.Input;

namespace Athan.Avalonia.Features.Shell;

internal sealed partial class ShellView : Window
{
    private readonly ShellViewModel viewModel;

    public ShellView(ShellViewModel viewModel)
    {
        this.viewModel = viewModel;

        DataContext = viewModel;
        InitializeComponent();
    }

    private async void CaptionBorderOnPointerReleased(object? sender, PointerReleasedEventArgs eventArgs)
    {
        var border = (CaptionBorder) sender!;

        if (border.Type is CaptionType.Close)
        {
            Close();
        }
        else
        {
            ShowInTaskbar = false;
            WindowState = WindowState.Minimized;

            await viewModel.MinimizingCommand.ExecuteAsync(sender);
        }
    }
}