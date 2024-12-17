using System.Windows.Controls;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersView : UserControl
{
    public PrayersView(PrayersViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}