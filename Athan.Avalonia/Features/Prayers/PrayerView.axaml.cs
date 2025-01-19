using Athan.Avalonia.Extensions;
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

        viewModel.PropertyChanged += (_, _) =>
        {
            var opacity = viewModel.Next is null ? 0 : 1;

            NextBorder.Opacity = opacity;
            PrayerList.Opacity = opacity;
        };
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        viewModel.StartAsync().Await();
    }

    private void PrayerSelectionChanged(object? sender, SelectionChangedEventArgs eventArgs)
    {
        PrayerList.SelectedItem = viewModel.Next;
    }
}