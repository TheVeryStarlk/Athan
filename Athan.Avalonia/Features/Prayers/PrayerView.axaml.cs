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
    }

    protected override void OnInitialized()
    {
        viewModel.Initialize();
        base.OnInitialized();
    }
}