using Microsoft.UI.Xaml.Controls;

namespace Athan.UI.Features.Prayers;

internal sealed partial class PrayerView : Page
{
    public PrayerViewModel ViewModel { get; }

    public PrayerView(PrayerViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();
    }
}