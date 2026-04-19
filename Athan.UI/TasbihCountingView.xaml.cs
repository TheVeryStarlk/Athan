using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI;

internal sealed partial class TasbihCountingView : Page
{
    private TasbihCountingViewModel? viewModel;

    public TasbihCountingView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        viewModel = (TasbihCountingViewModel) eventArgs.Parameter;

        base.OnNavigatedTo(eventArgs);
    }
}