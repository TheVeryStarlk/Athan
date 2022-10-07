using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class OfflineView : UserControl
{
    public OfflineView()
    {
        DataContext = App.Current.Services.GetRequiredService<OfflineViewModel>();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}