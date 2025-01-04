using Athan.Avalonia.ViewModels;
using Avalonia.Controls;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    public ShellView(ShellViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}