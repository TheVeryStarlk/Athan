using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class LocationView : UserControl
{
    private readonly LocationViewModel viewModel = App.Current.Services.GetRequiredService<LocationViewModel>();

    public LocationView()
    {
        DataContext = viewModel;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
        viewModel.GetLocationCommand.ExecuteAsync(null);
    }
}