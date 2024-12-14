using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Welcome;

internal sealed partial class WelcomeView : Page
{
    public WelcomeViewModel ViewModel { get; }

    public WelcomeView(WelcomeViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}