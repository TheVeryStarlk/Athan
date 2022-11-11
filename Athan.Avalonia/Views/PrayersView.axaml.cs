using Athan.Avalonia.Extensions;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Athan.Avalonia.Views;

internal sealed partial class PrayersView : UserControl
{
    public PrayersView()
    {
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