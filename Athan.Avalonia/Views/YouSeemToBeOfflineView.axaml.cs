using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class YouSeemToBeOfflineView : UserControl
{
    public YouSeemToBeOfflineView()
    {
        DataContext = App.Current.Services.GetRequiredService<YouSeemToBeOfflineViewModel>();
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}