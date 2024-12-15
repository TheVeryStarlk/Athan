using System.Windows.Controls;

namespace Athan.Desktop.Features.Welcome;

public sealed partial class WelcomeView : UserControl
{
    public WelcomeView(WelcomeViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}