using Avalonia.Controls;

namespace Athan.Avalonia.Features.Errors;

internal sealed partial class ErrorView : UserControl
{
    public ErrorView(ErrorViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}