using System.Windows;
using System.Windows.Controls;
using Serilog;

namespace Athan.Desktop.Features.Prayers;

public sealed partial class PrayersView : UserControl
{
    private readonly ILogger logger;
    private readonly PrayersViewModel viewModel;

    public PrayersView(ILogger logger, PrayersViewModel viewModel)
    {
        this.logger = logger;
        this.viewModel = viewModel;

        viewModel.PropertyChanged += (_, _) =>
        {
            WaitTextBlock.Visibility = this.viewModel.Prayers is null ? Visibility.Visible : Visibility.Collapsed;
            MainStackPanel.Visibility = this.viewModel.Prayers is null ? Visibility.Collapsed : Visibility.Visible;
        };

        DataContext = viewModel;
        InitializeComponent();
    }

    protected override async void OnInitialized(EventArgs eventArgs)
    {
        try
        {
            base.OnInitialized(eventArgs);
            await viewModel.InitializeAsync();
        }
        catch (Exception exception)
        {
            logger.Error("An error has occured in initialization: {Message}", exception.Message);
        }
    }

    private void PrayersSelectionChanged(object sender, SelectionChangedEventArgs eventArgs)
    {
        PrayersListView.SelectedItem = viewModel.NextPrayer;
    }
}