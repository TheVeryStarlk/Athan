using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI;

internal sealed partial class TasbihView : Page
{
    private TasbihViewModel? viewModel;

    public TasbihView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        viewModel = (TasbihViewModel) eventArgs.Parameter;
        base.OnNavigatedTo(eventArgs);
    }
}