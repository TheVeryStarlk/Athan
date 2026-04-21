using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayersView : Page
{
    private PrayersViewModel? viewModel;

    public PrayersView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs eventArgs)
    {
        viewModel = (PrayersViewModel) eventArgs.Parameter;
        base.OnNavigatedTo(eventArgs);
    }
}