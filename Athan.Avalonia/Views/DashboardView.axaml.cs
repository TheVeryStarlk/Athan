using Athan.Avalonia.Extensions;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Athan.Avalonia.Views;

internal sealed partial class DashboardView : UserControl
{
    public DashboardView()
    {
        DataContext = ViewModelLocator.DashboardViewModel;
        InitializeComponent();
    }

    protected override async void OnInitialized()
    {
        await this.AnimateAsync();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}