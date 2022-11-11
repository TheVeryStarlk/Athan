using Athan.Avalonia.Extensions;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Athan.Avalonia.Views;

internal sealed partial class SettingsView : UserControl
{
    public SettingsView()
    {
        DataContext = ViewModelLocator.SettingsViewModel;
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