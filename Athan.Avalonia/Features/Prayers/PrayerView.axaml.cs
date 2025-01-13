using System;
using Avalonia.Controls;
using Serilog;

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

    protected override async void OnInitialized()
    {
        try
        {
            await viewModel.StartAsync();
            base.OnInitialized();
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "A fatal exception occured.");
        }
    }

    private void SelectingItemsControlOnSelectionChanged(object? sender, SelectionChangedEventArgs eventArgs)
    {
        PrayerList.SelectedItem = viewModel.Next;
    }
}