using Athan.Avalonia.ViewModels;
using Avalonia.Controls;

namespace Athan.Avalonia.Views;

internal sealed partial class ErrorView : UserControl
{
    public ErrorView(ErrorViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}