using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class LocationView : UserControl
{
    public LocationView()
    {
        DataContext = App.Current.Services.GetRequiredService<LocationViewModel>();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}