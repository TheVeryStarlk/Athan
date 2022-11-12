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

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}