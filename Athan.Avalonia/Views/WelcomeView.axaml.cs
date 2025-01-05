using Athan.Avalonia.ViewModels;
using Avalonia.Controls;

namespace Athan.Avalonia.Views;

internal sealed partial class WelcomeView : UserControl
{
    public WelcomeView(WelcomeViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}