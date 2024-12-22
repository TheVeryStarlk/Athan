using System.Windows;
using System.Windows.Controls;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersView : UserControl
{
    private readonly PrayersViewModel viewModel;

    public PrayersView(PrayersViewModel viewModel)
    {
        this.viewModel = viewModel;

        viewModel.PropertyChanged += (_, _) => Dispatcher.Invoke(() =>
        {
            WaitTextBlock.Visibility = this.viewModel.Prayers is null ? Visibility.Visible : Visibility.Collapsed;
            MainStackPanel.Visibility = this.viewModel.Prayers is null ? Visibility.Collapsed : Visibility.Visible;
            PrayersListView.SelectedItem = viewModel.NextPrayer;
        });

        DataContext = viewModel;
        InitializeComponent();
    }

    private void PrayersSelectionChanged(object sender, SelectionChangedEventArgs eventArgs)
    {
        PrayersListView.SelectedItem = viewModel.NextPrayer;
    }
}