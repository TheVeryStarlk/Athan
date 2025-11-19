using Avalonia.Controls;

namespace Athan.Avalonia.Features.Prayers;

internal sealed partial class PrayerView : UserControl
{
    private readonly PrayerViewModel viewModel;

    public PrayerView(PrayerViewModel viewModel)
    {
        this.viewModel = viewModel;

        DataContext = viewModel;
        InitializeComponent();

        viewModel.PropertyChanged += (_, _) => ContainerStackPanel.Opacity = viewModel.Next is null ? 0 : 1;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        _ = viewModel.StartAsync();
    }

    private void PrayerSelectionChanged(object? sender, SelectionChangedEventArgs eventArgs)
    {
        PrayerList.SelectedItem = viewModel.Next;
    }
}