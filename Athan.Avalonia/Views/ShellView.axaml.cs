using Athan.Avalonia.ViewModels;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Athan.Avalonia.Views;

internal sealed partial class ShellView : Window
{
    public ShellView()
    {
        DataContext = App.Current.Services.GetRequiredService<ShellViewModel>();
        InitializeComponent();

        Margin = OffScreenMargin;
    }
}