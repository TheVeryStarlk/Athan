using Athan.Avalonia.ViewModels;
using Avalonia.Controls;

namespace Athan.Avalonia.Views;

internal sealed partial class PrayerView : UserControl
{
    public PrayerView(PrayerViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}